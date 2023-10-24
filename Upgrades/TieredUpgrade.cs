using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public partial class TieredUpgrade<TModifier> : BuyableUpgrade<TModifier> where TModifier : IdleModifier , new()
{
	[JsonProperty]
	protected int tier;

	public int GetTier() => tier;

	public override void OnBuy()
	{
        tier++;
        base.OnBuy();
    }

	public override bool IsOneTimeBuy()
	{
		return false;
	}

	public override bool IsMaxed()
	{
		return false;
	}
}
