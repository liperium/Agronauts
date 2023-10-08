using Godot;
using System;

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
		return new Pos2D(other2.X + other.X, other2.Y + other.Y);
	}
	public static Pos2D operator -(Pos2D other, Pos2D other2)
	{
		return new Pos2D(other2.X - other.X, other2.Y - other.Y);
	}
}
