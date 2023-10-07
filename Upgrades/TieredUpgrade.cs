using Godot;
using System;
[Serializable]
public partial class TieredUpgrade<TModifier> : BuyableUpgrade<TModifier> where TModifier : IdleModifier , new()
{
	public int tier;

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
