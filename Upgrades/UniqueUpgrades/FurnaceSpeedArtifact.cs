using Godot;
using System;

public partial class FurnaceSpeedArtifact : BuyableUpgrade<MultiplierModifier>
{
    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.furnaceSpeed;
    }

    public override void OnLoad()
    {
        if (IsUnlocked() == false) GameState.instance.numbers.fightWave.SetOnValueChanged(CheckUnlock);
        base.OnLoad();
    }

    public void CheckUnlock(long wave)
    {
        if (wave >= 2)
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
    
    public override void UpdateModifier()
    {
        modifier.multiplier = 3f;
    }

    public override void UpdateCost()
    {
        cost = 0;
    }


    public override void InitInfo()
    {
        base.InitInfo();
        info.SetName("KFURNACESPEEDARTIFACT");
        info.SetDescription("KFURNACESPEEDARTIFACTDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/CarburantInterstellaire.png");
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Artifact;
    }
}
