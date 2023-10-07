using Godot;
using System;

public partial class TotalPotatoYieldUpgrade : TieredUpgrade <MultiplierModifier>
{
	public override void Buy()
	{
		if (CanBuy())
		{
			modifier.multiplier += 0.05f;
			tier++;
		}
	}

	public override bool CanBuy()
	{
		return true;
	}

	public long GetCost()
	{
		return (long)(5 + Mathf.Pow(1.1,tier) + tier);
	}
}
