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

        public override string ToString()
        {
            return this.Name;
        }
    }
}