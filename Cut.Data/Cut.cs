/*
 * Created by SharpDevelop.
 * User: kiril
 * Date: 10.07.2017
 * Time: 21:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;


namespace Cut.Data
{
	/// <summary>
	/// Description of Cut.
	/// Отрезок - балка, стойка и т.п.
	/// </summary>
	public class Cut
	{
		public Cut()
		{
		}
		
		public string Name { get; set; }
		public int Length { get; set; }
		public Size PlankSize { get; set; }
		
		/// <summary>
		/// Доска, из которой делают этот отрезок 
		/// </summary>
		/// <value></value>
		public Plank Plank { get; set; }
		
		public override string ToString()
		{
			return string.Format("[Cut Name={0}, Length={1}]", Name, Length);
		}

	}
}
