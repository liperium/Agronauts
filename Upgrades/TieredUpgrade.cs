using Godot;
using System;

public partial class TieredUpgrade<TModifier> : BuyableUpgrade<TModifier> where TModifier : IdleModifier 
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
}
