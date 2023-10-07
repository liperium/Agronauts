using Godot;
using System;

public partial class TotalPotatoYieldUpgrade : TieredUpgrade <MultiplierModifier>
{
	public override void Buy()
	{
		if (CanBuy())
		{
			//Pay cost
			modifier.multiplier += 0.05f;
		}
	}

	public override bool CanBuy()
	{
		return base.CanBuy();
	}

	public long GetCost()
	{
		return (long)(5 + Mathf.Pow(1.1,tier));
	}
}
