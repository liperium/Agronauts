using Godot;
using System;

public partial class TotalPotatoYieldUpgrade : TieredUpgrade
{
	public override void Buy()
	{
		if (CanBuy())
		{

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
