/*
 * Created by SharpDevelop.
 * User: Tall
 * Date: 10.07.2017
 * Time: 21:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Cut.Data
{
	/// <summary>
	/// Description of Plank.
	/// Доска, из которой делают отрезки
	/// </summary>
	public class Plank
	{
		public Plank(PlankSet plankSet)
		{
			if (plankSet == null)
				throw new ArgumentNullException("plankSet");
			
			this.PlankSet = plankSet;
			this.Length = plankSet.PlankLength;
			
			this.Cuts = new List<Cut>();
			
		}
		
		/// <summary>
		/// Набор досок одного размера
		/// </summary>
		/// <value></value>
		public PlankSet PlankSet { get; private set; }
		
		/// <summary>
		/// Длина доски
		/// </summary>
		/// <value></value>
		public int Length { get; set; }
		
		public long Volume {
			get { return this.Length * this.PlankSet.PlankSize.Width * this.PlankSet.PlankSize.Height; }
		}

		public long AVolume {
			get { return (this.PlankSet.LibItem != null ? this.Length * this.PlankSet.LibItem.AWidth * this.PlankSet.LibItem.AHeight : 0);}
		}
		
		/// <summary>
		/// Список отрезков
		/// </summary>
		/// <value></value>
		public List<Cut> Cuts { get; private set; }
		
		public void AddCut(Cut cut){
			cut.Plank = this;
			this.Cuts.Add(cut);
		}
		
		public int GetUsedLength (int bladeWidth) {
			int res = 0;
			foreach(Cut c in this.Cuts){
				res += c.Length + bladeWidth;
			}
			
			return res - bladeWidth;
		}
		
		public int GetUnusedLength(int bladeWidth) {
			return this.Length - this.GetUsedLength(bladeWidth);
		}
	}
}