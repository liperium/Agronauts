using Godot;
using System;

public partial class AutoFurnaceUpgrade : BuyableUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {      
        base.OnBuy();
        GamePopUp.instance.AddToQueue(new GamePopUpInfo("KCONGRATULATIONS","KCOOKUNLOCKTIP",ResourceLoader.Load<CompressedTexture2D>("res://Upgrades/UpgradeImages/Cuisinier.png")));
    }

    public override void UpdateCost()
    {
        cost = 1000;
    }

    public override IdleNumber GetCostNumber()
    {
        return GameState.instance.numbers.cookedPotatoCount;
    }

    public override void Apply()
    {
        return;
    }

    public override void InitInfo()
    {
        base.InitInfo();
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
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Furnace;
    }
}
