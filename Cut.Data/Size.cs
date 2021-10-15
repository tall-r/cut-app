/*
 * Created by SharpDevelop.
 * User: kiril
 * Date: 10.07.2017
 * Time: 21:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Cut.Data
{
	/// <summary>
	/// Description of Size.
	/// Размер доски, отрезка, когда стоит на ребре
	/// Например: Width=50, Height=150
	/// </summary>
	public class Size: IComparable, IEquatable<Size>
	{
		public Size()
		{
			
		}
		
		public Size(int width, int height)
		{
			if (width <= 0)
				throw new ArgumentOutOfRangeException("width", width, "Value must be great that 0");
			if (height <= 0)
				throw new ArgumentOutOfRangeException("height", height, "Value must be great that 0");
			this.Width = width;
			this.Height = height;
			
		}
		
		public int Width { get; set; }
		public int Height { get; set; }
		
		int IComparable.CompareTo(object obj){
			if (obj is Size) 
				return CompareTo((Size)obj);

			throw new ArgumentException("Invalid object type.", "obj");
		}
		
		bool IEquatable<Size>.Equals(Size size){
			return this.Equals(size);
		}
		
		public int CompareTo(Size size){
			if ((size.Width == this.Width) && (size.Height == this.Height))
				return 0;
			else{
				if (size.Width == this.Width)
					return this.Height.CompareTo (size.Height);
				if (size.Height == this.Height)
					return this.Width.CompareTo (size.Width);
				
				int q1 = this.Height * this.Width;
				int q2 = size.Height * size.Width;
				return q1.CompareTo (q2);
			}
		}
		
		public bool Equals(Size size){
			return ((size.Width == this.Width) && (size.Height == this.Height));
		}
		
		public override string ToString()
		{
			return string.Format("[Size Width={0}, Height={1}]", Width, Height);
		}

	}
}
