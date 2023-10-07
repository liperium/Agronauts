using Godot;
using System;

public partial class TieredUpgrade : BuyableUpgrade
{
	int tier;

	public override void Buy()
	{
		base.Buy();
	}

	public override bool CanBuy()
	{
		return base.CanBuy();
	}
}
