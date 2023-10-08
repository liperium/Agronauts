using Godot;
using System;
[Serializable]
public partial class TotalPotatoYieldUpgrade : TieredUpgrade <MultiplierModifier>
{

	public override void OnBuy()
	{
		modifier.multiplier++;
        base.OnBuy();
    }

    public override void UpdateCost()
	{
		cost = (long)(5 + Mathf.Pow(1.1, tier) + tier);
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
		info.SetImagePath("res://Icons/Potato.png");
	}

	public override void OnLoad()
	{
		base.OnLoad();
		unlocked = true;
	}


}
