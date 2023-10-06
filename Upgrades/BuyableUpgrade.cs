using Godot;
using System;

public partial class BuyableUpgrade : IBuyable
{
    public virtual void Buy()
	{
	}

    public virtual bool CanBuy()
	{
		return false;
	}
}
