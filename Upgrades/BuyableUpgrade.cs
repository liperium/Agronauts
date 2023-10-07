using Godot;
using System;

public partial class BuyableUpgrade<TModifier> : IdleUpgrade<TModifier>, IBuyable where TModifier : IdleModifier
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
