/*
 * Created by SharpDevelop.
 * User: Tall
 * Date: 10.07.2017
 * Time: 23:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Cut.Data
{
	/// <summary>
	/// Description of Util.
	/// </summary>
	public static class Util
	{
		public static bool Between(this int value, int min, int max) {
			return ((value >= min) && (value <= max));
		}
		
		
		public static List<PlankSet> GetPlankSets(IEnumerable<Cut> cuts, int plankLength){
			var sizes = new List<Size>();
			foreach(Cut c in cuts){
				if (!sizes.Contains(c.PlankSize))
					sizes.Add (c.PlankSize);
			}
			
			var plankSets = new List<PlankSet>();
			foreach(Size s in sizes){
				plankSets.Add(new PlankSet(s, plankLength));
			}
			
			return plankSets;
		}
		
		public static IEnumerable<Cut> GetUnusedCuts(IEnumerable<Cut> cuts, Size plankSize, int minLen, int maxLen) {
			return cuts
					.Where(x => (x.Plank == null))
					.Where(x => (x.PlankSize.Equals(plankSize)))
					.Where(x => (x.Length.Between(minLen, maxLen)))
					.OrderByDescending(x => x.Length);
		}
		
		public static void CalculatePlankSet(Settings s, PlankSet plankSet, IEnumerable<Cut> cuts){
			
			var cutList = GetUnusedCuts(cuts, plankSet.PlankSize, 1, plankSet.PlankLength);
			
			while (cutList.Count() > 0) {
				
				Plank p = plankSet.AddPlank();
				
				while (true){
					int unusedLen = p.GetUnusedLength(s.BladeWidth);
					if (unusedLen.Between(0, s.MaxCutOff - 1))
						break;
					
					cutList = GetUnusedCuts(cuts, plankSet.PlankSize, 1, unusedLen);
					if (cutList.Count() > 0)
						p.AddCut(cutList.First());
					else
						break;
				}
				
				cutList = GetUnusedCuts(cuts, plankSet.PlankSize, 1, plankSet.PlankLength);
			}
			//plankSet.Planks = plankSet.Planks.OrderBy(p => p.Cuts[0]);
		}
		
		public static IEnumerable<PlankSet> GenerateCutList(Settings cfg, PlankLib lib, IEnumerable<Cut> cuts){
			List<PlankSet> plankSets = Util.GetPlankSets(cuts, cfg.DefaultPlankLength);
			
			foreach (var ps in plankSets) {
				
				var libItem = lib.FindItem(ps.PlankSize);
				if (libItem != null) {
					
					ps.LibItem = libItem;
					ps.PlankLength = libItem.Length;
				}
				
				Util.CalculatePlankSet(cfg, ps, cuts);
			}
			
			return plankSets;
		}
		
		public static IEnumerable<Cut> LoadCuts(string fileName, System.Text.Encoding enc){
			using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open)) {
				return LoadCuts(fs, enc);
			}
		}

		public static IEnumerable<Cut> LoadCuts(System.IO.Stream stream, System.Text.Encoding enc) {
			List<Cut> list = new List<Cut>();
			
			using(System.IO.StreamReader sr = new System.IO.StreamReader(stream, enc)){
				string firstLine = sr.ReadLine();
				List<string> fields = new List<string>();
				fields.AddRange(firstLine.Split(';'));
				
				while (!sr.EndOfStream){
					string buf = sr.ReadLine();
					string[] values = buf.Split(';');
					
					Size plankSize = new Size();
					plankSize.Width = Convert.ToInt32(values[fields.IndexOf("Width")]);
					plankSize.Height = Convert.ToInt32(values[fields.IndexOf("Height")]);
					
					string name = values[fields.IndexOf("Name")];
					int cutLength = Convert.ToInt32(values[fields.IndexOf("Length")]);
					int qty = Convert.ToInt32(values[fields.IndexOf("Qty")]);
					
					for(int i = 0; i < qty; i++)
					{
						Cut c = new Cut();
						c.Name = name;
						c.Length = cutLength;
						c.PlankSize = plankSize;
						
						list.Add(c);
					}
				}
			}
			return list;
		}

		public static IEnumerable<Cut> LoadCuts(System.IO.Stream stream) {
			return LoadCuts(stream, System.Text.Encoding.UTF8);
		}
		
		public static IEnumerable<Cut> LoadCuts(string fileName){
			return LoadCuts(fileName, System.Text.Encoding.GetEncoding(1251));
		}
	}
}
