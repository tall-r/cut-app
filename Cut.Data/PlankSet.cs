/*
 * Created by SharpDevelop.
 * User: kiril
 * Date: 10.07.2017
 * Time: 23:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Cut.Data
{
	/// <summary>
	/// Description of PlankSet.
	/// Набор досок одного размера, например 50х100, 50х150
	/// </summary>
	public class PlankSet
	{
		public PlankSet()
		{
			this.Planks = new List<Plank>();
		}
		
		public PlankSet(Size plankSize, int plankLength)
			: this()
		{
			if (plankSize == null)
				throw new ArgumentNullException("plankSize");
			
			if (plankLength <= 0)
				throw new ArgumentOutOfRangeException("plankLength", plankLength, "Value must be great that 0");

			this.PlankSize = plankSize;
			this.PlankLength = plankLength;
			
		}
		
		public PlankSet(Size plankSize)
			: this(plankSize, 6000) {
		
		}
		
		/// <summary>
		/// Размер доски
		/// </summary>
		/// <value></value>
		public Size PlankSize { get; set; }
		
		/// <summary>
		/// Длина доски
		/// </summary>
		/// <value></value>
		public int PlankLength { get; set; }
		
		/// <summary>
		/// Связанный тип доски из библиотеки
		/// </summary>
		/// <value></value>
		public PlankLibItem LibItem { get; set; }
		
		/// <summary>
		/// Список досок
		/// </summary>
		/// <value></value>
		public List<Plank> Planks { get; private set; }
		
		public Plank AddPlank(){
			Plank p = new Plank(this);
			this.Planks.Add(p);
			
			return p;
		}
		
		public long Volume {
			get {
				long res = 0;
				this.Planks.ForEach(p => { res += p.Volume; } );
				return res;
			}
		}
		public long AVolume {
			get {
				long res = 0;
				this.Planks.ForEach(p => { res += p.AVolume; } );
				return res;
			}
		}

		public override string ToString() {
			return this.LibItem != null ? this.LibItem.Name : string.Format("{0}x{1}x{2}", this.PlankSize.Width, this.PlankSize.Height, this.PlankLength);
		}
	}
}
