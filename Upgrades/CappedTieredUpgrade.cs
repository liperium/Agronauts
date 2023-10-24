using System;
using WJA23Godot.Upgrades;

[Serializable]
public partial class CappedTieredUpgrade<TModifier> : TieredUpgrade<TModifier>, ICappedUpgrade
	where TModifier : IdleModifier , new()
{

	public virtual long GetTierCap()
	{
		return 1;
	}
	
	public override bool IsMaxed()
	{
		return tier == GetTierCap();
	}

	public override string GetEffectText()
	{
		return "("+tier+"/"+GetTierCap()+") ";
	}
}
