using Godot;
using System;

public partial class AutoFurnaceUpgrade : BuyableUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {      
        base.OnBuy();
        unlock_pop_up.instance.ChangeText("KCONGRATULATIONS", "KCOOKUNLOCKTIP");
        unlock_pop_up.instance.ChangeImage("res://Upgrades/UpgradeImages/Cuisinier.png");
        unlock_pop_up.instance.Animation();
    }

    public override void UpdateCost()
    {
        cost = 10000;
    }

    public override IdleNumber GetCostNumber()
    {
        return GameState.instance.numbers.cookedPotatoCount;
    }

    public override void Apply()
    {
        return;
    }

    public override void InnitInfo()
    {
        base.InnitInfo();
        info.SetName("KAUTOCOOKUPGRADE");
        info.SetDescription("KAUTOCOOKUPGRADEDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/Cuisinier.png");
    }

    public override void OnLoad()
    {
        base.OnLoad();

        if (IsUnlocked() == false)
        {
            GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(CheckUnlock);
        }
    }

    public void CheckUnlock(long cookedPotatos)
    {
        if (cookedPotatos >= 100)
        {
            Unlock();
            GameState.instance.numbers.cookedPotatoCount.ResetOnValueChanged(CheckUnlock);

        }
    }
}
