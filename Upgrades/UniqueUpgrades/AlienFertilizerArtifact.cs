using Godot;
using System;

public partial class AlienFertilizerArtifact : BuyableUpgrade<MultiplierModifier>
{
    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.potatoYield;
    }

    public override void OnLoad()
    {
        if (IsUnlocked() == false) GameState.instance.numbers.fightWave.SetOnValueChanged(CheckUnlock);
        base.OnLoad();
    }

    public void CheckUnlock(long wave)
    {
        if (wave >= 3)
        {
            GameState.instance.numbers.fightWave.ResetOnValueChanged(CheckUnlock);
            Unlock();
            Buy();
        }
    }

    public override void OnBuy()
    {
        modifier.multiplier = 2f;
        base.OnBuy();
    }

    public override void UpdateCost()
    {
        cost = 0;
    }


    public override void InnitInfo()
    {
        base.InnitInfo();
        info.SetName("KFURNACESPEEDARTIFACT");
        info.SetDescription("KFURNACESPEEDARTIFACTDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/CarburantInterstellaire.png");
    }
}
