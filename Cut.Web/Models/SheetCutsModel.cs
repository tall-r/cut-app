using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cut.Data;

namespace Cut.Web.Models
{
    public class SheetCutsModel
    {
        public Cut.Data.Size SheetSize { get; set; }

        public IList<Cut.Data.Sheet> Sheets { get; set; }
        
        public IList<Cut.Data.SheetCut> WrongSheets { get; set; }
        
        
    }
}