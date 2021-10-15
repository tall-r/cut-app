/*
 * Created by SharpDevelop.
 * User: Tall
 * Date: 12.07.2017
 * Time: 13:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Serialization;
using System.ComponentModel;
using System.IO;

namespace Cut.Data
{
	/// <summary>
	/// Description of PlankLib.
	/// Библиотека типовых размеров досок, брусков и т.п.
	/// </summary>
	[Serializable]
	[XmlRoot("lib")]
	public class PlankLib
	{
		public PlankLib()
		{
			this.Items = new List<PlankLibItem>();
		}
		
		[XmlArray("planks")]
		[XmlArrayItem("plank")]
		public List<PlankLibItem> Items { get; set; }
		
		public PlankLibItem FindItem(Size size){
			if (this.Items.Count == 0) return null;
			
			return this.Items.Where(x => (x.Width == size.Width && x.Height == size.Height)).First();
		}
		
		public static PlankLib LoadLib(string fileName, Settings cfg){
			PlankLib lib = null;
			XmlSerializer ser = new XmlSerializer(typeof(PlankLib));
			using(StreamReader sr = new StreamReader(fileName)){
				lib = (PlankLib)ser.Deserialize(sr);
			}
			
			if (lib != null){
				foreach (var libItem in lib.Items) {
	
					if (libItem.Length == 0)
						libItem.Length = cfg.DefaultPlankLength;
					
					if (libItem.AWidth == 0)
						libItem.AWidth = libItem.Width;
					if (libItem.AHeight == 0)
						libItem.AHeight = libItem.Height;
	
				}
			}
			
			return lib;
		}
	}
	

	/// <summary>
	/// Элемент библиотеки, описывающий типую доску или брусок
	/// </summary>
	[Serializable]
	public class PlankLibItem{
		
		[XmlAttribute("name")]
		public string Name { get; set; }
		
		/// <summary>
		/// Фактический ширина доски
		/// </summary>
		/// <value></value>
		[XmlAttribute("width")]
		public int Width { get; set; }
		
		/// <summary>
		/// Фактическая высота доски
		/// </summary>
		/// <value></value>
		[XmlAttribute("height")]
		public int Height { get; set; }
		
		/// <summary>
		/// Длина доски
		/// </summary>
		/// <value></value>
		[XmlAttribute("length")]
		public int Length { get; set; }

		/// <summary>
		/// Расчетная (сырая) ширина доски
		/// Например, если доска шириной 45мм, то здесь можно указать 50мм, чтобы потом правильно расчитать кубатуру (кол-во досок в 1 м3)
		/// </summary>
		/// <value></value>
		[XmlAttribute("awidth")]
		public int AWidth { get; set; }
		
		/// <summary>
		/// Расчетная (сырая) высота доски
		/// Например, если доска высотой 145мм, то здесь можно указать 150мм, чтобы потом правильно расчитать кубатуру (кол-во досок в 1 м3)
		/// </summary>
		/// <value></value>
		[XmlAttribute("aheight")]
		public int AHeight { get; set; }
	}
}
