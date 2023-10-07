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
        GameState.instance.numbers.potatoCount.SetValue(100);

    }
    public override void _Pressed()
	{
		potatoYieldUpgrade.Buy();
		this.Text = string.Format(Tr("KUPFARMYIELD"), potatoYieldUpgrade.GetCost().FormattedNumber());
		//this.Text = "Upgrade Farm Yield (" + potatoYieldUpgrade.GetCost().ToString("#,#").Replace(',',' ') + ")";
	}

	public void UpdateEnabled(long number)
	{
		this.Disabled = !potatoYieldUpgrade.CanBuy();
	}
}
