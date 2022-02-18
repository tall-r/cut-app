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
    /// Описывает деталь
    /// </summary>
    public class SheetCut
    {
        public SheetCut(string name, Size size)
        {
            this.Name = name;
            this.Size = size;
        }

        public SheetCut()
            : this("", new Size(0,0)) 
        {

        }

        public string Name { get; set; }

        /// <summary>
        /// Height - длинная сторона листа
        /// </summary>
        /// <value></value>
        public Size Size { get; set; }

        public void SwapSize() {
            int h = this.Size.Height;
            this.Size.Height = this.Size.Width;
            this.Size.Width = h;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public Dictionary<string,string> AsJsonData() {
            Dictionary<string,string> res = new Dictionary<string, string>();
            res["name"] = this.Name;
            res["height"] = this.Size.Height.ToString();
            res["width"] = this.Size.Width.ToString();

            return res;
        }
    }
}