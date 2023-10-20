using Godot;
using System;
using WJA23Godot.Upgrades;

public partial class CappedArtifactUpgrade<TModifier> : ArtifactUpgrade<TModifier>, ICappedUpgrade
	where TModifier :  IdleModifier , new()
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
