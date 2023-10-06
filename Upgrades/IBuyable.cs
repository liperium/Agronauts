using Godot;
using Godot.Collections;
using System;

public interface IBuyable 
{
	public bool CanBuy();
	public void Buy();
}