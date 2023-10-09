using Godot;
using System;
[Serializable]
public partial class BuyableUpgrade<TModifier> : IdleUpgrade<TModifier>, IBuyable where TModifier : IdleModifier, new()
{
	protected long cost;
	
	public bool unlocked;
    
	private Action<long> OnCostChanged;
	private Action OnUnlock;
	private Action OnBuyUpgrade;

	protected IdleNumber costNumber;
	
	#region Action Setters
	public void SetOnCostChanged(Action<long> action)
	{
		OnCostChanged += action;
	}
	
	public void ResetOnCostChanged(Action<long> action)
	{
		OnCostChanged -= action;
	}
	
	public void SetOnUnlock(Action action)
	{
		OnUnlock += action;
	}
	
	public void ResetOnUnlock(Action action)
	{
		OnUnlock -= action;
	}
	
	public void SetOnBuyUpgrade(Action action)
	{
		OnBuyUpgrade += action;
	}
	
	public void ResetOnBuyUpgrade(Action action)
	{
		OnBuyUpgrade -= action;
	}
	
	#endregion

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
			info.SetCostImagePath(costNumber.GetImagePath());
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
			acquired = true;
			
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
