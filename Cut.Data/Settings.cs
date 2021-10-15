/*
 * Created by SharpDevelop.
 * User: kiril
 * Date: 10.07.2017
 * Time: 21:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cut.Data
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	public class Settings
	{
		public Settings()
		{
			this.BladeWidth = 3;
			this.MaxCutOff = 40;
			this.DefaultPlankLength = 6000;
		}
		
		[DefaultValue(3)]
		public int BladeWidth { get; set; }
		
		/// <summary>
		/// Обрезок
		/// </summary>
		[DefaultValue(40)]
		public int MaxCutOff { get; set; }
		
		[DefaultValue(6000)]
		public int DefaultPlankLength { get; set; }
	}
}
