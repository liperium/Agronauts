using Godot;
using System;

public partial class AutoCookLevel3Upgrade : BuyableUpgrade<MultiplierModifier>
{
    public override void UpdateModifier()
    {
        modifier.multiplier = 3f;
    }

    public override void UpdateCost()
    {
        cost = 100000;
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
        info.SetName("KCOOKLVL3");
        info.SetDescription("KCOOKLVL3DESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/chapo1star.png");
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
        if (cookedPotatos >= 10000)
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
