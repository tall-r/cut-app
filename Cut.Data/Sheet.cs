/*
 * Created by VSCode.
 * User: Tall
 * Date: 15.10.2021
 * Time: 17:40
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cut.Data
{
    /// <summary>
    /// Лист с расположением на нем деталей
    /// </summary>
    public class Sheet
    {
        public Sheet()
        {
            this.Rows = new List<List<SheetCut>>();
        }

        public Sheet(Size sheetSize): this()
        {
            if (sheetSize.Height < sheetSize.Width)
                throw new Exception("Неверная высота листа. Высота должна быть длинее ширины.");
            this.Size = sheetSize;
        }
        /// <summary>
        /// Height - длинная сторона листа
        /// </summary>
        /// <value></value>
        public Size Size { get; set; }

        /// <summary>
        /// Положение детали на листе
        /// </summary>
        /// <value></value>
        //public Dictionary<SheetCut, Position> Cuts { get; private set; }

        public List<List<SheetCut>> Rows { get; private set; }

        /// <summary>
        /// Возвращает самую верхнюю линию/ряд деталей
        /// </summary>
        /// <returns></returns>
        public List<SheetCut> GetTopRow() {
            if (this.Rows.Count == 0) {
                List<SheetCut> row = new List<SheetCut>();
                this.Rows.Add(row);
                return row;
            }
            else
                return this.Rows[this.Rows.Count - 1];
        }

        public List<SheetCut> AddRow(int bladeWidth, int maxCutOff) {
            if (IsSheetFull(bladeWidth, maxCutOff))
                throw new NoMoreSpaceException();

            return AddRow();
        }

        public List<SheetCut> AddRow() {
            List<SheetCut> row = new List<SheetCut>();
            this.Rows.Add(row);

            return row;
        }
        

        /// <summary>
        /// Проверяет заполнение листа по высоте
        /// </summary>
        /// <param name="bladeWidth">Толщина лезвия пила/ширина пропила</param>
        /// <param name="maxCutOff">Максимально допустимый обрезок</param>
        /// <returns></returns>
        public bool IsSheetFull(int bladeWidth, int maxCutOff){
            return (GetSheetSpace(bladeWidth) <= maxCutOff);
        }

        /// <summary>
        /// Проверяет заполнение линии по ширине
        /// </summary>
        /// <param name="row">Линия/ряд деталей на листе, набор деталей</param>
        /// <param name="bladeWidth">Толщина лезвия пила/ширина пропила</param>
        /// <param name="maxCutOff">Максимально допустимый обрезок</param>
        /// <returns>Если </returns>
        public bool IsRowFull(List<SheetCut> row, int bladeWidth, int maxCutOff) {
            return (GetRowSpace(row, bladeWidth) <= maxCutOff);
        }

        public int GetSheetSpace(int bladeWidth) {
            return this.Size.Height - this.Rows.Sum(x => (x[0].Size.Height + bladeWidth));
        }

        public int GetRowSpace(List<SheetCut> row, int bladeWidth) {
            return this.Size.Width - row.Sum(x => (x.Size.Width + bladeWidth));
        }

        public Dictionary<string,object> AsJsonData() {
            Dictionary<string,object> data = new Dictionary<string, object>();

            data["height"] = this.Size.Height.ToString();
            data["width"] = this.Size.Width.ToString();
            List<List<Dictionary<string,string>>> rows = new List<List<Dictionary<string, string>>>();
            foreach(var row in this.Rows) {
                List<Dictionary<string,string>> r = new List<Dictionary<string, string>>();
                foreach(var c in row) {
                    r.Add(c.AsJsonData());
                }
                rows.Add(r);
            }
            data["rows"] = rows;

            return data;
        }
    }
}