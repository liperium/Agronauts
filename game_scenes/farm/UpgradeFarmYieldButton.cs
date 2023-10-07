using Godot;
using System;

public partial class UpgradeFarmYieldButton : Button
{

	TotalPotatoYieldUpgrade potatoYieldUpgrade;

	public override void _Ready()
	{
        potatoYieldUpgrade = GameState.instance.upgrades.totalPotatoYieldUpgrade;
		this.Text = "Upgrade Farm Yield (" + potatoYieldUpgrade.GetCost() + ")";
	}
	public override void _Pressed()
	{
		potatoYieldUpgrade.Buy();
        this.Text = "Upgrade Farm Yield (" + potatoYieldUpgrade.GetCost().ToString("#,#").Replace(',',' ') + ")";
	}
}
