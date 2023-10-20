using System;
using Godot;

public class InvasionTimeUpgrade : CappedTieredUpgrade<MultiplierModifier>
{
    public override void UpdateModifier()
    {
        modifier.multiplier = 1f - tier*0.1f;
    }

    public override void UpdateCost()
    {
        cost = (long)(10000 * Math.Pow(tier + 1, tier));
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.invasionTime;
    }

    public override void InitInfo()
    {
        base.InitInfo();

        info.SetName("KINVASIONTIMEUPGRADE");
        info.SetDescription("KINVASIONTIMEUPGRADEDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/radar.png");
    }

    public override long GetTierCap()
    {
        return 5;
    }

    public override void OnLoad()
    {
        base.OnLoad();
        if (acquired == false)
        {
            GameState.instance.numbers.fightWave.SetOnValueChanged(CheckUnlock);
        }
    }

    public void CheckUnlock(long fightWave)
    {
        if (fightWave >= 1)
        {
            Unlock();
            GameState.instance.numbers.fightWave.ResetOnValueChanged(CheckUnlock);
        }
    }

    public override string GetEffectText()
    {
        return base.GetEffectText() + GameState.instance.numbers.invasionTime.GetValue().FormattedNumber() + "s";
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Farm;
    }
}