using System;

[Serializable]
public class IdleUpgradeContainer
{
    public TotalPotatoYieldUpgrade totalPotatoYieldUpgrade;
    public FirstTractorUpgrade firstTractorUpgrade;
    public PotatoSpeedUpgrade potatoSpeedUpgrade;

    public void Init()
    {
        totalPotatoYieldUpgrade = new TotalPotatoYieldUpgrade();
        firstTractorUpgrade = new FirstTractorUpgrade();
        potatoSpeedUpgrade = new PotatoSpeedUpgrade();
    }

    public void OnLoad()
    {
        totalPotatoYieldUpgrade.OnLoad();
        firstTractorUpgrade.OnLoad();
        potatoSpeedUpgrade.OnLoad();
    }
}