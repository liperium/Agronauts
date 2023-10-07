using Godot;
using System;

public partial class UnlockFurnaceUpgrade : IdleUpgrade<MultiplierModifier>
{
	public override void Apply()
	{
		return;
	}
	public override void InnitInfo()
	{
		base.InnitInfo();
	}

	public override void OnLoad()
	{
		base.OnLoad();
		//Faire qu'au moment de la première invasion au unlock le four / upgrades de four
	}
}
