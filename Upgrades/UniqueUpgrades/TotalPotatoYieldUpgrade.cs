using Godot;
using System;
[Serializable]
public partial class TotalPotatoYieldUpgrade : TieredUpgrade <MultiplierModifier>
{
	public override void UpdateModifier()
	{
		modifier.multiplier = 1 + tier;
	}

	public override void UpdateCost()
	{
		cost = (long)(5 + Mathf.Pow(tier, 2) + tier);
	}

	public override IdleNumber GetAffectedNumber()
	{
		return GameState.instance.numbers.potatoYield;
	}

	public override void InnitInfo()
	{
		base.InnitInfo();
		
		info.SetName("KPOTATOYIELDUPGRADE");
		info.SetDescription("KPOTATOYIELDUPGRADEDESC");
		info.SetImagePath("res://Upgrades/UpgradeImages/patate+.png");
	}

	public override void OnLoad()
	{
		base.OnLoad();
		unlocked = true;
	}

    public override string GetEffectText()
    {
        return  + ((int)(modifier.multiplier * 100)) + "%";
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
	    return UIManager.UpgradeTab.Farm;
    }

}
