using Godot;
public partial class PotatoYieldArtifact : ArtifactUpgrade<MultiplierModifier>
{
	public override IdleNumber GetAffectedNumber()
	{
		return GameState.instance.numbers.potatoYield;
	}

	public override void UpdateModifier()
	{
		modifier.multiplier = 1 + Mathf.RoundToInt( 0.1f * tier);
	}

	public override void InitInfo()
	{
		base.InitInfo();
		info.SetName("KPOTATOYIELDARTIFACT");
		info.SetDescription("KPOTATOYIELDARTIFACTDESC");
		info.SetImagePath("res://Upgrades/UpgradeImages/Engrais.png");
	}

	public override int GetWeight()
	{
		return 1;
	}

	public override string GetEffectText()
	{
		return base.GetEffectText() + Mathf.RoundToInt((modifier.multiplier * 100)) + "%";
	}
}
