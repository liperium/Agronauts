using Godot;
using System;

[Serializable]
public class Pos2D
{
	public int X = 0;
	public int Y = 0;

	public Pos2D(int X, int Y)
	{
		this.X = X;
		this.Y = Y;
	}

	public static Pos2D operator +(Pos2D other, Pos2D other2)
	{
		return new Pos2D(other.X + other2.X, other.Y + other2.Y);
	}
	public static Pos2D operator -(Pos2D other, Pos2D other2)
	{
		return new Pos2D(other.X - other2.X, other.Y - other2.Y);
	}
}
