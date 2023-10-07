using Godot;
using System;
[Serializable]
public partial class BuyableUpgrade<TModifier> : IdleUpgrade<TModifier>, IBuyable where TModifier : IdleModifier, new()
{
	protected long cost;
	
	protected bool unlocked;

	public Action<long> OnCostChanged;
	
	public override void OnLoad()
	{
		base.OnLoad();
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
			SetAffectedNumber();
			Pay();
			OnBuy();

			long tempCost = cost;
			
			UpdateCost();

			if (cost != tempCost && OnCostChanged != null)
			{
				OnCostChanged(cost);
			}
		}
	}
    public virtual void OnBuy() {
	    affectedNumber.UpdateValue();
    }
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
