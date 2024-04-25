using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cut.Data
{
    public class SheetRow: List<SheetCut>
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("side")]
        public GroupingSide Side { get; set; }
    }
}