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

namespace Cut.Data
{
    public enum GroupSide {
        gsHeight,
        gsWidth
    }

    public static class SheetUtils
    {
        /// <summary>
        /// Группируем детали (Cut) по размеру одной из сторон
        /// </summary>
        /// <param name="list">Список деталей</param>
        /// <param name="gs">Сторона детали, по которой крупируются наборы</param>
        /// <returns></returns>
        public static Dictionary<int, List<SheetCut>> GroupCutsBySize(IEnumerable<SheetCut> list, GroupSide gs) {
            Dictionary<int, List<SheetCut>> dict = new Dictionary<int, List<SheetCut>>();

            foreach(SheetCut c in list) {
                int key = 0;
                if (gs == GroupSide.gsHeight)
                    key = c.Size.Height;
                else if(gs == GroupSide.gsWidth)
                    key  = c.Size.Width;
                else
                    break;

                List<SheetCut> cuts =null;

                if (dict.ContainsKey(key))
                    cuts = dict[key];

                if (cuts == null) {
                    cuts = new List<SheetCut>();
                    dict[key] = cuts;
                }

                cuts.Add(c);
            }

            return dict;
        }
        /// <summary>
        /// Выбираем набор с минимальным кол-вом групп
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rotationAllowed"></param>
        /// <returns></returns>
        public static Dictionary<int, List<SheetCut>> FindProperGroups(IEnumerable<SheetCut> list, bool rotationAllowed) {
            Dictionary<int, List<SheetCut>> hset = GroupCutsBySize(list, GroupSide.gsHeight);
            Dictionary<int, List<SheetCut>> wset = GroupCutsBySize(list, GroupSide.gsWidth);

            Dictionary<int, List<SheetCut>> rhset = null;
            Dictionary<int, List<SheetCut>> rwset = null;
            if (rotationAllowed) {
                rhset = GroupCutsBySize(list, GroupSide.gsHeight);
                rwset = GroupCutsBySize(list, GroupSide.gsWidth);
            }

            Dictionary<int, List<SheetCut>> result = (hset.Count <= wset.Count) ? hset : wset;
            if (rotationAllowed) {
                Dictionary<int, List<SheetCut>> rresult = (rhset.Count <= rwset.Count) ? rhset : rwset;

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
        public static List<SheetCut> GetInvalidSheetCuts(List<SheetCut> list, Size sheetSize) {
            return list.Where(c => (c.Size.Height > sheetSize.Height || c.Size.Width > sheetSize.Width)).ToList();
        }

        public static List<Sheet> GenerateSheets(List<SheetCut> list, List<SheetCut> wrongCuts, Size sheetSize, bool rotaionAllowed, Settings cfg) {
            List<Sheet> result = new List<Sheet>();

            wrongCuts = GetInvalidSheetCuts(list, sheetSize);
            var cuts = list.Where( c => (!wrongCuts.Contains(c)));

            Dictionary<int, List<SheetCut>> cutGroups = FindProperGroups(cuts, rotaionAllowed);

            List<SheetCut> usedCuts = new List<SheetCut>();

            Sheet lastSheet = null;

            foreach(int key in cutGroups.Keys.OrderByDescending(x => x)) {
                List<SheetCut> groupCuts = cutGroups[key];
                if (groupCuts != null && groupCuts.Count > 0) {
                    GroupSide side = (key == groupCuts[0].Size.Height) ? GroupSide.gsHeight : GroupSide.gsWidth;
                    Dictionary<int, List<SheetCut>> grps2 = GroupCutsBySize(groupCuts, side == GroupSide.gsHeight ? GroupSide.gsWidth : GroupSide.gsHeight);
                    foreach(int gkey in grps2.Keys.OrderByDescending(x => x)) {
                        List<SheetCut> gcuts = grps2[gkey];
                        foreach(SheetCut c in gcuts) {
                            if (usedCuts.Contains(c)) continue;

                            if (lastSheet == null) {
                                lastSheet = new Sheet(sheetSize);
                                result.Add(lastSheet);
                            }

                            List<SheetCut> row = lastSheet.GetTopRow();
                            if (lastSheet.IsRowFull(row, cfg.BladeWidth, cfg.MaxCutOff)) {
                                if (lastSheet.IsSheetFull(cfg.BladeWidth, cfg.MaxCutOff)) {
                                     result.Add(lastSheet);
                                     lastSheet = new Sheet(sheetSize);
                                }
                                row = lastSheet.AddRow();
                            }
                            else {
                                int space = lastSheet.GetRowSpace(row, cfg.BladeWidth);
                                if (space < c.Size.Width) {
                                    // find smaller cut
                                    try {
                                        SheetCut c1 = groupCuts.Where(x => !usedCuts.Contains(x)).Where(x => x.Size.Width <= space).OrderByDescending(x => x.Size.Width).First();

                                        if (c1 != null) {
                                            row.Add(c1);
                                            usedCuts.Add(c1);
                                        }
                                    }
                                    catch {

                                    } 

                                    if (lastSheet.IsSheetFull(cfg.BladeWidth, cfg.MaxCutOff)){
                                        lastSheet = new Sheet(sheetSize);
                                        result.Add(lastSheet);
                                    }
                                    row = lastSheet.AddRow();
                                    row.Add(c);
                                    usedCuts.Add(c);
                                }
                                else {
                                    row.Add(c);
                                    usedCuts.Add(c);
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}