using Godot;
using System;

public partial class TractorSpreadSeedsUpgrade : BuyableUpgrade<IdleModifier>
{
    public override void OnBuy()
    {      
        base.OnBuy();
    }


    public override void UpdateCost()
    {
        cost = 1000000;
    }

    public override void Apply()
    {
        return;
    }

    public override void InnitInfo()
    {
        base.InnitInfo();
        info.SetName("KPLANTER");
        info.SetDescription("KPLANTERDESC");
        info.SetImagePath("res://game_scenes/farm/tracteur/sprites/epandeur.png");
    }

    public override void OnLoad()
    {
        base.OnLoad();

        if (IsUnlocked() == false)
        {
            GameState.instance.numbers.truckPotatosFarmed.SetOnValueChanged(CheckUnlock);
        }
    }

    public void CheckUnlock(long potatos)
    {
        if (potatos >= 1000000)
        {
            Unlock();
            GameState.instance.numbers.truckPotatosFarmed.ResetOnValueChanged(CheckUnlock);
        }
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Farm;
    }
}
