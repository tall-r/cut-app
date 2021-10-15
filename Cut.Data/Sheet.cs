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

        /// <summary>
        /// Height - длинная сторона листа
        /// </summary>
        /// <value></value>
        public Size Size { get; set; }

        /// <summary>
        /// Положение детали на листе
        /// </summary>
        /// <value></value>
        public Dictionary<SheetCut, Position> Cuts { get; private set; }

    }
}