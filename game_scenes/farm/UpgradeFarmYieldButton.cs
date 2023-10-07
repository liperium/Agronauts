using Godot;
using System;

public partial class UpgradeFarmYieldButton : Button
{

	TotalPotatoYieldUpgrade potatoYieldUpgrade;

	public override void _Ready()
	{

	}
	public override void _Pressed()
	{
		GD.Print("UpgradeFarmYieldButtonPressed");
	}
}
