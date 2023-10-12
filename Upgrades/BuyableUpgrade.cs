using Godot;
using System;
[Serializable]
public partial class BuyableUpgrade<TModifier> : IdleUpgrade<TModifier>, IBuyable where TModifier : IdleModifier, new()
{
	protected long cost;
	
    
	private Action<long> OnCostChanged;
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

	public void SetOnBuyUpgrade(Action action)
	{
		OnBuyUpgrade += action;
	}
	
	public void ResetOnBuyUpgrade(Action action)
	{
		OnBuyUpgrade -= action;
	}

	public bool IsAcquired()
	{
		return acquired;
	}
	
	#endregion

	public override void OnLoad()
	{
		base.OnLoad();
		UpdateCost();
		SetCostNumber();
		UIManager.AddAllUpgrade(this);
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

    public virtual void Buy()
	{
		if (CanBuy())
		{
			Unlock();
			acquired = true;
			
			SetAffectedNumber();
			Pay();
			OnBuy();
			Apply();
			affectedNumber.UpdateValue();
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

    public virtual UIManager.UpgradeTab GetUpgradeTab()
    {
	    return UIManager.UpgradeTab.None;
    }

    public virtual bool IsOneTimeBuy()
    {
	    return true;
    }
}
