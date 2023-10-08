using Godot;
using System;

public partial class AutoFurnaceUpgrade : BuyableUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {      
        base.OnBuy();
    }

    public override void UpdateCost()
    {
        cost = 10000;
    }

    public override void Apply()
    {
        return;
    }

    public override void InnitInfo()
    {
        base.InnitInfo();
        info.SetName("KFIRSTTRACTORUPGRADE");
        info.SetDescription("KFIRSTTRACTORUPGRADEDESC");
        info.SetImagePath("res://Icons/Potato.png");
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
        if (cookedPotatos >= 1000)
        {
            Unlock();
            GameState.instance.numbers.cookedPotatoCount.ResetOnValueChanged(CheckUnlock);
        }
    }
}
