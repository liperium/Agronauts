using Godot;
using System;

public partial class TractorSpeedUpgrade : TieredUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        modifier.multiplier *= 2;
        base.OnBuy();
    }

    public override void UpdateCost()
    {
        cost =  (long)Mathf.Pow(1000f,tier+1);
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.truckSpeed;
    }

    public override void InnitInfo()
    {
        base.InnitInfo();

        info.SetName("KTRUCKSPEEDUPGRADE");
        info.SetDescription("KTRUCKSPEEDUPGRADEDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/CarburantPatate.png");
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
}
