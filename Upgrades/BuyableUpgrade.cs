using Godot;
using System;
[Serializable]
public partial class BuyableUpgrade<TModifier> : IdleUpgrade<TModifier>, IBuyable where TModifier : IdleModifier, new()
{
	protected long cost;
	
	protected bool unlocked;

	public BuyableUpgrade()
	{
		UpdateCost();
	}

	public bool IsUnlocked()
	{
		return unlocked;
	}
    public virtual void Buy()
	{
		if (CanBuy())
		{
			Pay();
			OnBuy();
			UpdateCost();
		}
	}
    public virtual void OnBuy() {}
    public virtual void Pay()
    {
	    GameState.instance.numbers.potatoCount.DecreaseValue(cost);
    }

    public virtual bool CanBuy()
	{
		GD.Print(GameState.instance.numbers.potatoCount.GetValue());
		return GameState.instance.numbers.potatoCount.GetValue() >= cost;
	}

    public virtual void UpdateCost() {}
    
    public long GetCost()
    {
	    return cost;
    }
}
