using Godot;
using System;

public partial class TotalPotatoYieldUpgrade : TieredUpgrade <MultiplierModifier>
{
	public override void OnBuy()
	{
		modifier.multiplier += 0.05f;
		tier++;
	}

	public override void UpdateCost()
	{
		cost = (long)(5 + Mathf.Pow(1.1, tier) + tier);
	}
}
