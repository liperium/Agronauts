using Godot;
using WJA23Godot.Upgrades;

public partial class CookedPotatoBatchArtifact : ArtifactUpgrade<MultiplierModifier>
{
	public override IdleNumber GetAffectedNumber()
	{
		return GameState.instance.numbers.furnaceBatchCount;
	}

	public override void UpdateModifier()
	{
		modifier.multiplier = 1 +  0.05f * tier;
	}

	public override void InitInfo()
	{
		base.InitInfo();
		info.SetName("KCOOKEDPOTATOBATCHARTIFACT");
		info.SetDescription("KCOOKEDPOTATOBATCHARTIFACTDESC");
		info.SetImagePath(InfoUpgrade.defaultImagePath);
	}

	public override int GetWeight()
	{
		return 100;
	}

	public override string GetEffectText()
	{
		return base.GetEffectText() + Mathf.RoundToInt((modifier.multiplier * 100)) + "%";
	}

	public override ArtifactRarity GetRarity()
	{
		return ArtifactRarity.Common;
	}
	
}
