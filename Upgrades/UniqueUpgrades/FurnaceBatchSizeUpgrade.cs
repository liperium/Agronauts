using Godot;
using System;

public partial class FurnaceBatchSizeUpgrade : TieredUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        modifier.multiplier += 1f;
        base.OnBuy();
    }

    public override void UpdateCost()
    {
        cost = (long)(100 + Mathf.Pow(1.1, tier)*tier + tier);
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.furnaceBatchCount;
    }

    public override void InnitInfo()
    {
        base.InnitInfo();

        info.SetName("KFURNACEBATCHCOUNT");
        info.SetDescription("KFURNACEBATCHCOUNTDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/four+.png");
    }

    public override void OnLoad()
    {
        base.OnLoad();

        if (IsUnlocked() == false) GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(CheckUnlock);
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
        return +((int)(modifier.multiplier*100))+"%";
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Furnace;
    }

}
