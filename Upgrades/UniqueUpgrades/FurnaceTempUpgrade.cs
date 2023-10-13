using Godot;
using System;

public partial class FurnaceTempUpgrade : TieredUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        modifier.multiplier *= 1.2f;
        base.OnBuy();
    }
    
    

    public override void UpdateCost()
    {
        cost = (long)(Mathf.Pow(10,tier+2));
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.potatoTemperature;
    }

    public override void InnitInfo()
    {
        base.InnitInfo();

        info.SetName("KFURNACETEMP");
        info.SetDescription("KFURNACETEMPDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/fourTemp.png");
    }

    public override IdleNumber GetCostNumber()
    {
        return GameState.instance.numbers.cookedPotatoCount;
    }

    public override void OnLoad()
    {
        base.OnLoad();

        if (IsUnlocked() == false) GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(CheckUnlock);
    }

    private void CheckUnlock(long value)
    {
        if (value > 10)
        {
            Unlock();
            GameState.instance.numbers.cookedPotatoCount.ResetOnValueChanged(CheckUnlock);
        }
    }

    public override string GetEffectText()
    {
        return  + ((int)(modifier.multiplier * 100)) + "%";
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Furnace;
    }
}
