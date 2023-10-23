using Godot;
using System;

public partial class FurnaceBatchSizeUpgrade : CappedTieredUpgrade<MultiplierModifier>
{
    
    public override void UpdateModifier()
    {
        modifier.multiplier = 1 + tier;
    }

    public override void UpdateCost()
    {
        cost = (long)(100 + Mathf.Pow(tier,2f) + tier);
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.furnaceBatchCount;
    }

    public override void InitInfo()
    {
        base.InitInfo();

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

    public override long GetTierCap()
    {
        return 500;
    }

    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Furnace;
    }

}
