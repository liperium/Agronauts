using Godot;
using System;

public partial class TractorSpeedUpgrade : CappedTieredUpgrade<MultiplierModifier>
{
    public override void UpdateModifier()
    {
        modifier.multiplier = Mathf.Pow(2, tier);
    }

    public override void UpdateCost()
    {
        cost =  (long)Mathf.Pow(500f,tier+1);
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.truckSpeed;
    }

    public override void InitInfo()
    {
        base.InitInfo();

        info.SetName("KTRUCKSPEEDUPGRADE");
        info.SetDescription("KTRUCKSPEEDUPGRADEDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/CarburantPatate.png");
    }

    public override long GetTierCap()
    {
        return 3;
    }

    public override void OnLoad()
    {
        base.OnLoad();
        GameState.instance.numbers.truckAmount.SetOnValueChanged(CheckUnlock);
    }

    public void CheckUnlock(long tiles)
    {
        Unlock();
        GameState.instance.numbers.truckAmount.ResetOnValueChanged(CheckUnlock);
    }

    public override string GetEffectText()
    {
        return ""+base.GetEffectText() +(Mathf.RoundToInt(modifier.multiplier * 100)) + "%";
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Farm;
    }
}
