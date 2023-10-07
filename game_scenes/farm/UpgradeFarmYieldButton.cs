using Godot;
using System;

public partial class UpgradeFarmYieldButton : Button
{
	TotalPotatoYieldUpgrade potatoYieldUpgrade;

	public override void _Ready()
	{
        potatoYieldUpgrade = GameState.instance.upgrades.totalPotatoYieldUpgrade;
		this.Text = string.Format(Tr("KUPFARMYIELD"), potatoYieldUpgrade.GetCost().FormattedNumber());
		GameState.instance.numbers.potatoCount.SetOnValueChanged(UpdateEnabled);
		UpdateEnabled(potatoYieldUpgrade.GetAffectedNumber().GetValue());

    }
    public override void _Pressed()
	{
		//potatoYieldUpgrade.Buy();
		//this.Text = string.Format(Tr("KUPFARMYIELD"), potatoYieldUpgrade.GetCost().FormattedNumber());
		//this.Text = "Upgrade Farm Yield (" + potatoYieldUpgrade.GetCost().ToString("#,#").Replace(',',' ') + ")";
		GameState.instance.upgrades.firstTractorUpgrade.Buy();
	}

	public void UpdateEnabled(long number)
	{
		this.Disabled = !potatoYieldUpgrade.CanBuy();
	}
}
