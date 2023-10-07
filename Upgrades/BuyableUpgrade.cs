using Godot;
using System;

public partial class BuyableUpgrade : IBuyable
{
	protected bool unlocked;


	public bool IsUnlocked()
	{
		return unlocked;
	}
    public virtual void Buy()
	{
	}

    public virtual bool CanBuy()
	{
		return false;
	}
}
