using Godot;
using System;

public partial class AutoCookLevel3Upgrade : BuyableUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        modifier.multiplier = 3f;
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
        info.SetName("KCOOKLVL3");
        info.SetDescription("KCOOKLVL3DESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/Cuisinier.png");
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
        if (cookedPotatos >= 1000000)
        {
            Unlock();
            GameState.instance.numbers.furnaceTotalAutoCookedPotato.ResetOnValueChanged(CheckUnlock);
        }
    }
}
