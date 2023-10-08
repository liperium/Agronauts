using Godot;
using System;
[Serializable]
public partial class BuyableUpgrade<TModifier> : IdleUpgrade<TModifier>, IBuyable where TModifier : IdleModifier, new()
{
	protected long cost;
	
	public bool unlocked;
    
	public Action<long> OnCostChanged;
	public Action OnUnlock;
	public Action OnBuyUpgrade;

	protected IdleNumber costNumber;

	public override void OnLoad()
	{
		base.OnLoad();
		UpdateCost();
		SetCostNumber();
	}

    protected void SetCostNumber()
    {
        if (costNumber == null)
        {
            costNumber = GetCostNumber();
        }
    }

    public virtual IdleNumber GetCostNumber()
    {
        return GameState.instance.numbers.potatoCount;
    }

    public bool IsUnlocked()
	{
		return unlocked;
	}

	public virtual bool CanUnlock()
	{
		return true;
	}

	public void Unlock()
	{
		unlocked = true;
		if (OnUnlock != null) OnUnlock();
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
	    if (OnBuyUpgrade != null) OnBuyUpgrade();
    }
    public virtual void Pay()
    {
	    costNumber.DecreaseValue(cost);
    }

    public virtual bool CanBuy()
	{
		GD.Print(costNumber.GetValue() + " | "+cost);
		return costNumber.GetValue() >= cost;
	}

    public virtual void UpdateCost() {}
    
    public long GetCost()
    {
	    return cost;
    }
}
