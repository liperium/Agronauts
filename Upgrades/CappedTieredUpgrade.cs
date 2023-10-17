using Godot;
using System;
[Serializable]
public partial class CappedTieredUpgrade<TModifier> : TieredUpgrade<TModifier> where TModifier : IdleModifier , new()
{
	/// <summary>
	/// Gets the tier cap of the upgrade.
	/// </summary>
	/// <returns>The tier cap of the upgrade.</returns>
	protected virtual long GetTierCap()
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
