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
        cost = (long)(500 + Mathf.Pow(3f, tier) + 10 * tier);
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
        info.SetImagePath("res://Upgrades/UpgradeImages/FOUR.png");
    }

    public override void OnLoad()
    {
        base.OnLoad();

        if (IsUnlocked() == false) GameState.instance.numbers.fightWave.SetOnValueChanged(CheckUnlock);
    }

    private void CheckUnlock(long value)
    {
        if (GameState.instance.numbers.fightWave.GetValue() > 1)
        {
            Unlock();
            GameState.instance.numbers.fightWave.ResetOnValueChanged(CheckUnlock);
        }
    }
}