/*
 * Created by SharpDevelop.
 * User: Tall
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

	public class Position {

		public Position(){
			
		}

		public Position(int x, int y){
			this.X = x;
			this.Y = y;
		}

		public int X { get; set; }
		public int Y { get; set; }

		public override string ToString(){
			return string.Format("{0}:{1}", this.X, this.Y);
		}

		public void MoveX(int deltaX) {
			this.X += deltaX;
		}

		public void MoveY(int deltaY) {
			this.Y += deltaY;
		}

		/// <summary>
		/// Смещает текущую точку
		/// </summary>
		/// <param name="s"></param>
		public void Move(Size s) {
			this.X += s.Width;
			this.Y += s.Height;
		}

		/// <summary>
		/// Возвращает новую точку, смещенную от текущей на указаный размер
		/// </summary>
		/// <param name="s">Размер, на который сместить</param>
		/// <returns></returns>
		public Position Shift(Size s) {
			Position p = new Position(this.X, this.Y);
			p.Move(s);
			return p;
		}

	}
}
