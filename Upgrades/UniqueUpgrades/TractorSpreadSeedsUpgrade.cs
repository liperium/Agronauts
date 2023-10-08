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
        cost = 5000000;
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
        if (potatos >= 10000000)
        {
            Unlock();
            GameState.instance.numbers.truckPotatosFarmed.ResetOnValueChanged(CheckUnlock);
        }
    }
}
