using Godot;
using System;

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

	public override void UpdateCost()
	{
		cost = 0;
	}

	//Uniquement, cet artefact est obligatoirement dropped en gagnant la premiere wave
	public override void OnLoad()
	{
		base.OnLoad();
		if (IsUnlocked() == false)
		{
			GameState.instance.numbers.fightWave.SetOnValueIncreased(CheckUnlock);
		}
	}

	private void CheckUnlock(long value)
	{
		Buy();
		GameState.instance.numbers.fightWave.ResetOnValueIncreased(CheckUnlock);
	}


	public override void InitInfo()
	{
		base.InitInfo();
		info.SetName("KPOTATOYIELDARTIFACT");
		info.SetDescription("KPOTATOYIELDARTIFACTDESC");
		info.SetImagePath("res://Upgrades/UpgradeImages/Engrais.png");
	}
	public override UIManager.UpgradeTab GetUpgradeTab()
	{
		return UIManager.UpgradeTab.Artifact;
	}
}
