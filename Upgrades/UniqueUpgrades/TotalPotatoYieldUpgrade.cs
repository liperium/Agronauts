using Godot;
using System;

public partial class TotalPotatoYieldUpgrade : TieredUpgrade <MultiplierModifier>
{

	public override void OnBuy()
	{
        modifier.multiplier += 0.05f;
        base.OnBuy();
    }

    public override void UpdateCost()
	{
		cost = (long)(5 + Mathf.Pow(1.1, tier) + tier);
	}

	public override void SetAffectedNumber()
	{
		affectedNumber = GameState.instance.numbers.potatoYield;
	}

	public override void InnitInfo()
	{
		base.InnitInfo();

	}


}
