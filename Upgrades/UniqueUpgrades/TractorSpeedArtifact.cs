using Godot;
using System;

public partial class TractorSpeedArtifact : BuyableUpgrade<MultiplierModifier>
{
	public override IdleNumber GetAffectedNumber()
	{
		return GameState.instance.numbers.truckSpeed;
	}

	public override void OnLoad()
	{
		if (IsUnlocked() == false) GameState.instance.numbers.fightWave.SetOnValueChanged(CheckUnlock);
		base.OnLoad();
	}

	public void CheckUnlock(long wave)
	{
		if(wave >= 4)
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
        info.SetName("KTRUCKSPEEDARTIFACT");
        info.SetDescription("KTRUCKSPEEDARTIFACTDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/moteurextraterrestre.png");
    }


}
