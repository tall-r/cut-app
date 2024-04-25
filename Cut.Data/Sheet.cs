/*
 * Created by VSCode.
 * User: Tall
 * Date: 15.10.2021
 * Time: 17:40
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
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
            this.Rows = new List<SheetRow>();
            this.SheetSize = new Size();
        }

        public Sheet(Size sheetSize): this()
        {
            if (sheetSize.Height < sheetSize.Width)
                throw new Exception("Неверная высота листа. Высота должна быть длинее ширины.");
            this.SheetSize = sheetSize;
        }
        /// <summary>
        /// Height - длинная сторона листа
        /// </summary>
        /// <value></value>
        [JsonIgnore]
        public Size SheetSize { get; set; }

        [JsonPropertyName("height")]
        public int Height {
            get { return this.SheetSize.Height; }
            set { this.SheetSize.Height = value; }
        }

        [JsonPropertyName("width")]
        public int Width {
            get { return this.SheetSize.Width; }
            set { this.SheetSize.Width = value; }
        }

        /// <summary>
        /// Положение детали на листе
        /// </summary>
        /// <value></value>
        //public Dictionary<SheetCut, Position> Cuts { get; private set; }

        public IList<SheetRow> Rows { get; private set; }

        /// <summary>
        /// Возвращает самую верхнюю линию/ряд деталей
        /// </summary>
        /// <returns></returns>
        public SheetRow GetTopRow() {
            if (this.Rows.Count == 0) {
                SheetRow row = new SheetRow();
                this.Rows.Add(row);
                return row;
            }
            else
                return this.Rows[this.Rows.Count - 1];
        }

        public SheetRow AddRow(int bladeWidth, int maxCutOff) {
            if (IsSheetFull(bladeWidth, maxCutOff))
                throw new NoMoreSpaceException();

            return AddRow();
        }

        public SheetRow AddRow(int? rowHeight = null) {
            SheetRow row = new SheetRow();
            if (rowHeight.HasValue)
                row.Height = rowHeight.Value;
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
        public bool IsRowFull(IList<SheetCut> row, int bladeWidth, int maxCutOff) {
            return (GetRowSpace(row, bladeWidth) <= maxCutOff);
        }

        public int GetSheetSpace(int bladeWidth) {
            return this.Height - this.Rows.Sum(x => (x.Height + bladeWidth));
        }

        public int GetRowSpace(IList<SheetCut> row, int bladeWidth) {
            return this.Width - row.Sum(x => (x.Width + bladeWidth));
        }

        // public Dictionary<string,object> AsJsonData() {
        //     Dictionary<string,object> data = new Dictionary<string, object>();

        //     data["height"] = this.Height.ToString();
        //     data["width"] = this.Width.ToString();
        //     List<List<Dictionary<string,string>>> rows = new List<List<Dictionary<string, string>>>();
        //     foreach(var row in this.Rows) {
        //         List<Dictionary<string,string>> r = new List<Dictionary<string, string>>();
        //         foreach(var c in row) {
        //             r.Add(c.AsJsonData());
        //         }
        //         rows.Add(r);
        //     }
        //     data["rows"] = rows;

        //     return data;
        // }
    }
}