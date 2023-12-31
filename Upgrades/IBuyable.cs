using Godot;
using Godot.Collections;
using System;

public interface IBuyable
{
	public bool CanBuy();
	public void Buy();
	public void OnBuy();
	public void Pay();
	public void UpdateCost();
	public long GetCost();
	public InfoUpgrade GetInfo();
	public string GetEffectText();
	public bool IsUnlocked();
	public void SetOnUnlock(Action action);
	public void ResetOnUnlock(Action action);
	public void SetOnCostChanged(Action<long> action);
	public void ResetOnCostChanged(Action<long> action);
	public UIManager.UpgradeTab GetUpgradeTab();
	public bool IsOneTimeBuy();
	public void SetOnBuyUpgrade(Action action);
	public void ResetOnBuyUpgrade(Action action);
	public void SetOnInfoChanged(Action action);
	public void ResetOnInfoChanged(Action action);
	public void SetOnMaxedUpgrade(Action action);
	public void ResetOnMaxedUpgrade(Action action);
	public bool IsAcquired();
	public bool IsMaxed();
	public string GetBgStyle();
	public void InitInfo();

}