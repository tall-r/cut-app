/*
 * Created by VSCode.
 * User: Tall
 * Date: 18.02.2022
 * Time: 13:40
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;

namespace Cut.Data
{
    public enum GroupingSide {
        gsHeight,
        gsWidth
    }

    public static class SheetUtils
    {
        /// <summary>
        /// Группируем детали (Cut) по размеру одной из сторон
        /// </summary>
        /// <param name="list">Список деталей</param>
        /// <param name="gs">Сторона детали, по которой групируются наборы</param>
        /// <returns></returns>
        public static Dictionary<int, IList<SheetCut>> GroupCutsBySize(IEnumerable<SheetCut> list, GroupingSide gs) {
            Dictionary<int, IList<SheetCut>> dict = new Dictionary<int, IList<SheetCut>>();
            var groups = list.GroupBy(x => gs == GroupingSide.gsHeight ? x.Size.Height : x.Size.Width);

            foreach(var g in groups) {
                dict.Add(g.Key, g.ToList());
            }
            return dict;

            // foreach(SheetCut c in list) {
            //     int key = 0;
            //     if (gs == GroupSide.gsHeight)
            //         key = c.Size.Height;
            //     else if(gs == GroupSide.gsWidth)
            //         key  = c.Size.Width;
            //     else
            //         break;

            //     IList<SheetCut> cuts =null;

            //     if (dict.ContainsKey(key))
            //         cuts = dict[key];

            //     if (cuts == null) {
            //         cuts = new List<SheetCut>();
            //         dict[key] = cuts;
            //     }

            //     cuts.Add(c);
            // }

            // return dict;
        }
        /// <summary>
        /// Выбираем набор с минимальным кол-вом групп
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rotationAllowed"></param>
        /// <returns></returns>
        public static Dictionary<int, IList<SheetCut>> FindProperGroups(IEnumerable<SheetCut> list, bool rotationAllowed) {
            Dictionary<int, IList<SheetCut>> hset = GroupCutsBySize(list, GroupingSide.gsHeight);
            Dictionary<int, IList<SheetCut>> wset = GroupCutsBySize(list, GroupingSide.gsWidth);

            Dictionary<int, IList<SheetCut>> rhset = null;
            Dictionary<int, IList<SheetCut>> rwset = null;
            if (rotationAllowed) {
                // rhset = GroupCutsBySize(list, GroupingSide.gsHeight);
                // rwset = GroupCutsBySize(list, GroupingSide.gsWidth);
                rhset = new Dictionary<int, IList<SheetCut>>(wset);
                rwset = new Dictionary<int, IList<SheetCut>>(hset);
            }

            Dictionary<int, IList<SheetCut>> result = (hset.Count <= wset.Count) ? hset : wset;
            if (rotationAllowed) {
                Dictionary<int, IList<SheetCut>> rresult = (rhset.Count <= rwset.Count) ? rhset : rwset;

                if (result.Count > rresult.Count)
                    result = rresult;
            }

            return result;
        }

        /// <summary>
        /// Отбирает детали (Cuts), размеры которых больше размера листа
        /// </summary>
        /// <param name="list">Список деталей</param>
        /// <param name="sheetSize">Размеры листа</param>
        /// <returns></returns>
        public static IList<SheetCut> GetInvalidSheetCuts(IList<SheetCut> list, Size sheetSize) {
            return list.Where(c => (c.Size.Height > sheetSize.Height || c.Size.Width > sheetSize.Width)).ToList();
        }

        public static IList<Sheet> GenerateSheets(IList<SheetCut> list, IList<SheetCut> wrongCuts, Size sheetSize, bool rotaionAllowed, Settings cfg) {
            IList<Sheet> result = new List<Sheet>();

            wrongCuts = GetInvalidSheetCuts(list, sheetSize);
            var cuts = list.Where( c => (!wrongCuts.Contains(c)));

            Dictionary<int, IList<SheetCut>> cutGroups = FindProperGroups(cuts, rotaionAllowed);

            IList<SheetCut> usedCuts = new List<SheetCut>();

            Sheet lastSheet = null;

            foreach(int rowHeight in cutGroups.Keys.OrderByDescending(x => x)) {
                IList<SheetCut> groupCuts = cutGroups[rowHeight];
                if (groupCuts != null && groupCuts.Count > 0) {
                    GroupingSide side = (rowHeight == groupCuts[0].Size.Height) ? GroupingSide.gsHeight : GroupingSide.gsWidth;
                    Dictionary<int, IList<SheetCut>> grps2 = GroupCutsBySize(groupCuts, side == GroupingSide.gsHeight ? GroupingSide.gsWidth : GroupingSide.gsHeight);
                    foreach(int gkey in grps2.Keys.OrderByDescending(x => x)) {
                        IList<SheetCut> gcuts = grps2[gkey];
                        foreach(SheetCut c in gcuts) {
                            if (usedCuts.Contains(c)) continue;

                            if (lastSheet == null) {
                                lastSheet = new Sheet(sheetSize);
                                result.Add(lastSheet);
                            }

                            SheetRow row = lastSheet.GetTopRow();
                            if (lastSheet.IsRowFull(row, cfg.BladeWidth, cfg.MaxCutOff)) {
                                if (lastSheet.IsSheetFull(cfg.BladeWidth, cfg.MaxCutOff)) {
                                     result.Add(lastSheet);
                                     lastSheet = new Sheet(sheetSize);
                                }
                                row = lastSheet.AddRow(rowHeight);
                                row.Side = side;
                            }
                            // else {
                            int space = lastSheet.GetRowSpace(row, cfg.BladeWidth);
                            if (space < c.Size.Width) {
                                // find smaller cut
                                try {
                                    while(true){
                                        SheetCut c1 = groupCuts.Where(x => !usedCuts.Contains(x)).Where(x => x.Size.Width <= space).OrderByDescending(x => x.Size.Width).First();

                                        if (c1 != null) {
                                            row.Add(c1);
                                            usedCuts.Add(c1);
                                        }
                                        else
                                            break;
                                        space = lastSheet.GetRowSpace(row, cfg.BladeWidth);
                                    }
                                }
                                catch {

                                } 

                                if (lastSheet.IsSheetFull(cfg.BladeWidth, cfg.MaxCutOff)){
                                    lastSheet = new Sheet(sheetSize);
                                    result.Add(lastSheet);
                                }
                                row = lastSheet.AddRow(rowHeight);
                                row.Side = side;
                                row.Add(c);
                                usedCuts.Add(c);
                            }
                            else {
                                row.Add(c);
                                usedCuts.Add(c);
                            }
                            // }
                        }
                    }
                }
            }

            return result;
        }
    }
}