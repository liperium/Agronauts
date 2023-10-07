using Godot;
using System;

public partial class TieredUpgrade<TModifier> : BuyableUpgrade<TModifier> where TModifier : IdleModifier , new()
{
	protected int tier;

	public override void Buy()
	{
		base.Buy();
	}

	public override bool CanBuy()
	{
		return base.CanBuy();
	}

	public override void OnBuy()
	{
		base.OnBuy();
        tier++;
        if (tier == 1) Apply();
    }
}
