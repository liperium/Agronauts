using Godot;
using System;
[Serializable]
public partial class CappedTieredUpgrade<TModifier> : TieredUpgrade<TModifier> where TModifier : IdleModifier , new()
{
	public int tierCap;

	public override bool IsMaxed()
	{
		return tier == tierCap;
	}
	
	public override string GetEffectText()
	{
		return "("+tier+"/"+tierCap+") ";
	}
}
