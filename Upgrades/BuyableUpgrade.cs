using Godot;
using System;
using WJA23Godot.GameState;

[Serializable]
public partial class BuyableUpgrade<TModifier> : IdleUpgrade<TModifier>, IBuyable where TModifier : IdleModifier, new()
{
	protected long cost;
	
	private IdleAction<long> OnCostChanged;
	private IdleAction OnInfoChanged;
	private IdleAction OnMaxedUpgrade;
	private IdleAction OnBuyUpgrade;

	protected IdleNumber costNumber;

	public BuyableUpgrade()
	{
		OnCostChanged = new();
		OnInfoChanged = new();
		OnMaxedUpgrade = new();
		OnBuyUpgrade = new();
	}

	#region Action Setters
	public void SetOnCostChanged(Action<long> action)
	{
		OnCostChanged += action;
	}
	
	public void ResetOnCostChanged(Action<long> action)
	{
		OnCostChanged.RemoveManual(action);
	}

	public void SetOnBuyUpgrade(Action action)
	{
		OnBuyUpgrade += action;
	}
	
	public void ResetOnBuyUpgrade(Action action)
	{
		OnBuyUpgrade.RemoveManual(action);
	}
	public void SetOnInfoChanged(Action action)
	{
		OnInfoChanged += action;
	}
	
	public void ResetOnInfoChanged(Action action)
	{
		OnInfoChanged.RemoveManual(action);
	}
	
	public void SetOnMaxedUpgrade(Action action)
	{
		OnMaxedUpgrade += action;
	}
	
	public void ResetOnMaxedUpgrade(Action action)
	{
		OnMaxedUpgrade.RemoveManual(action);
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
			UpdateModifier();
			Apply();
			affectedNumber.UpdateValue();
			long tempCost = cost;
			CheckMaxed();
			UpdateCost();
			OnUpdateInfo();
			if (cost != tempCost)
			{
				OnCostChanged.Invoke(cost);
			}
		}
	}

    public override void Unlock()
    {
	    base.Unlock();
	    if (GameState.gameScene != GameScene.Farm)
	    {
		    UIManager.AddNewUnlocksToFlash(this);
	    }
    }

    public void CheckMaxed()
    {
	    if (IsMaxed())
	    {
		    OnMaxedUpgrade.Invoke();
	    }
    }
    
    public void OnUpdateInfo()
    {
	    OnInfoChanged.Invoke();
    }
    public virtual void OnBuy() {
	    affectedNumber.UpdateValue();
	    OnBuyUpgrade.Invoke();
    }
    public virtual void Pay()
    {
	    costNumber.DecreaseValue(cost);
    }

    public virtual bool CanBuy()
	{
		return costNumber.GetValue() >= cost && !IsMaxed();
	}

    /// <summary>
    /// Updates the cost. Called on load and on buy
    /// </summary>
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

    public virtual bool IsMaxed()
    {
	    return acquired;
    }

    public override string GetEffectText()
    {
	    if (acquired)
	    {
		    return " (1/1) ";
	    }
	    else
	    {
		    return " (0/1) ";
	    }
    }

    /// <summary>
    /// returns Style of the background of the upgrade can be overriden for unique styles for certain upgrades.
    /// </summary>
    /// <returns>Style of the background</returns>
    public virtual string GetBgStyle()
    {
	    return "DefaultUpgrade";
    }
}
