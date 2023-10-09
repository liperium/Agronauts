using Godot;
using System;

public partial class TempTractorSpeedUpgrade : BuyableUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        GameState.instance.numbers.potatoTemperature.SetOnValueChanged(UpdateMultiplier);
        modifier.multiplier = 1 + GameState.instance.numbers.potatoTemperature.GetValue() / 100f;
        base.OnBuy();
    }

    public override void UpdateCost()
    {
        cost = 100000;
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.potatoYield;
    }

    public override IdleNumber GetCostNumber()
    {
        return GameState.instance.numbers.cookedPotatoCount;
    }

    public override void InnitInfo()
    {
        base.InnitInfo();
        info.SetName("KTEMPSPEEDTRUCK");
        info.SetDescription("KTEMPSPEEDTRUCKDESC");
        info.SetImagePath(InfoUpgrade.defaultPath);
    }

    public override void OnLoad()
    {
        base.OnLoad();

        if (IsUnlocked() == false)
        {
            GameState.instance.numbers.furnaceBatchCount.SetOnValueChanged(CheckUnlock);
        }
        else
        {
            GameState.instance.numbers.potatoTemperature.SetOnValueChanged(UpdateMultiplier);
        }
    }

    public void UpdateMultiplier(long temp)
    {
        modifier.multiplier = 1 + GameState.instance.numbers.potatoTemperature.GetValue() / 100f;
    }

    public void CheckUnlock(long yield)
    {
        if (yield >= 50)
        {
            Unlock();
            GameState.instance.numbers.furnaceBatchCount.ResetOnValueChanged(CheckUnlock);
        }
    }
}
