using Godot;
using System;
using WJA23Godot.Upgrades;

public partial class DamageIncreaseArtifact : ArtifactUpgrade<MultiplierModifier>
{
	public override IdleNumber GetAffectedNumber()
	{
		return GameState.instance.numbers.damage;
	}

	public override void UpdateModifier()
	{
		modifier.multiplier = 1 +  0.1f * tier;
	}

	public override void InitInfo()
	{
		base.InitInfo();
		info.SetName("KDAMAGEARTIFACT");
		info.SetDescription("KDAMAGEARTIFACTDESC");
		info.SetImagePath(InfoUpgrade.defaultImagePath);
	}

	public override int GetWeight()
	{
		return 25;
	}

	public override string GetEffectText()
	{
		return base.GetEffectText() + Mathf.RoundToInt((modifier.multiplier * 100)) + "%";
	}

	public override ArtifactRarity GetRarity()
	{
		return ArtifactRarity.Uncommon;
	}
}
