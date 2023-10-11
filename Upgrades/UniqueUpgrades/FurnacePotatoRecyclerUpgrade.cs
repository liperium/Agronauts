using Godot;
using System;

public partial class FurnacePotatoRecyclerUpgrade : TieredUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        base.OnBuy();
        GameState.instance.numbers.cookedPotatoCount.SetOnValueIncreased(Recycle);
    }


    public override void UpdateCost()
    {
        cost = (long)(100 + Mathf.Pow(tier*4, 3f) + tier);
    }

    public override IdleNumber GetCostNumber()
    {
        return GameState.instance.numbers.cookedPotatoCount;
    }

    public override void InnitInfo()
    {
        base.InnitInfo();

        info.SetName("KCOOKEDRECYCLER");
        info.SetDescription("KCOOKEDRECYCLERDESC");
        info.SetImagePath(InfoUpgrade.defaultPath);
    }

    public override void Apply()
    {
        return;
    }

    public override void OnLoad()
    {
        base.OnLoad();

        if (IsUnlocked() == false) GameState.instance.upgrades.autoFurnaceUpgrade.SetOnUnlock(CheckUnlock);

        if (acquired)
        {
            GameState.instance.numbers.cookedPotatoCount.SetOnValueIncreased(Recycle);
        }
    }

    private void CheckUnlock()
    {
        Unlock();
        GameState.instance.upgrades.autoFurnaceUpgrade.ResetOnUnlock(CheckUnlock);     
    }

    public override string GetEffectText()
    {
        return +((int)(modifier.multiplier * 10)) + "%";
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Furnace;
    }

    public void Recycle(long number)
    {
        GameState.instance.numbers.potatoCount.IncreaseValue((long)(number * tier * 0.1f));

    }

}
