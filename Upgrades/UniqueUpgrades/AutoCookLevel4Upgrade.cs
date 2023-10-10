using Godot;
using System;

public partial class AutoCookLevel4Upgrade : BuyableUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        modifier.multiplier = 2f;
        base.OnBuy();
    }

    public override void UpdateCost()
    {
        cost = 10000000;
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.furnaceAutoBakeSpeed;
    }

    public override IdleNumber GetCostNumber()
    {
        return GameState.instance.numbers.cookedPotatoCount;
    }


    public override void InnitInfo()
    {
        base.InnitInfo();
        info.SetName("KCOOKLVL4");
        info.SetDescription("KCOOKLVL4DESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/chapo2star.png");
    }

    public override void OnLoad()
    {
        base.OnLoad();

        if (IsUnlocked() == false)
        {
            GameState.instance.numbers.furnaceTotalAutoCookedPotato.SetOnValueChanged(CheckUnlock);
        }
    }

    public void CheckUnlock(long cookedPotatos)
    {
        if (cookedPotatos >= 100000)
        {
            Unlock();
            GameState.instance.numbers.furnaceTotalAutoCookedPotato.ResetOnValueChanged(CheckUnlock);

        }
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Furnace;
    }
}
