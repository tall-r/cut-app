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

        public SheetCut(string name, int width, int height)
            :this(name, new Size(width, height)) {

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

        public static List<SheetCut> LoadCuts(System.IO.Stream stream, System.Text.Encoding enc){
            List<SheetCut> cuts = new List<SheetCut>();
            using(System.IO.StreamReader sr = new System.IO.StreamReader(stream, enc)) {
  				string firstLine = sr.ReadLine();
				List<string> fields = new List<string>();
				fields.AddRange(firstLine.Split(';'));
				
				while (!sr.EndOfStream){
					string buf = sr.ReadLine();
					string[] values = buf.Split(';');

                    string name = values[fields.IndexOf("Name")];
                    int qty = 0;
                    if (!int.TryParse(values[fields.IndexOf("Qty")], out qty)) {
                        qty = 0;
                    }

                    int width = Convert.ToInt32(values[fields.IndexOf("Width")]);
                    int length = Convert.ToInt32(values[fields.IndexOf("Length")]);

                    for (int i=1; i <= qty; i++) {
                        SheetCut cut = new SheetCut(name, width, length);
                        cuts.Add(cut);
                    }
                }
            }

            return cuts;
        }
    }
}