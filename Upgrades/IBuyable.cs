using Godot;
using Godot.Collections;
using System;

public interface IBuyable 
{
	public bool CanBuy();
	public void Buy();
	public void OnBuy();
	public void Pay();
	public void UpdateCost();
}