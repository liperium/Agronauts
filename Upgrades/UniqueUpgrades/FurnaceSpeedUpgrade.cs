using Godot;
using System;

public partial class FurnaceSpeedUpgrade : TieredUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        modifier.multiplier += 0.1f;
        base.OnBuy();
    }

    public override void UpdateCost()
    {
        cost = (long)(300 + Mathf.Pow(2.5f, tier) + 5 * tier);
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.furnaceSpeed;
    }

    public override void InnitInfo()
    {
        base.InnitInfo();

        info.SetName("KFURNACESPEED");
        info.SetDescription("KFURNACESPEEDDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/fourSPEED.png");
    }

    public override void OnLoad()
    {
        base.OnLoad();

       if(IsUnlocked() == false) GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(CheckUnlock);
    }

    private void CheckUnlock(long value)
    {
        if (GameState.instance.numbers.cookedPotatoCount.GetValue() > 0)
        {
            Unlock();
            GameState.instance.numbers.cookedPotatoCount.ResetOnValueChanged(CheckUnlock);
        }
    }

    public override string GetEffectText()
    {
        return  + ((int)(modifier.multiplier * 100)) + "%";
    }
}
