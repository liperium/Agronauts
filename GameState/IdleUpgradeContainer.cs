using System;

[Serializable]
public class IdleUpgradeContainer
{
    public TotalPotatoYieldUpgrade totalPotatoYieldUpgrade;
    public FirstTractorUpgrade firstTractorUpgrade;
    public PotatoSpeedUpgrade potatoSpeedUpgrade;
    public AddAutomaticTractorUpgrade addAutomaticTractorUpgrade;

    public void Init()
    {
        totalPotatoYieldUpgrade = new TotalPotatoYieldUpgrade();
        firstTractorUpgrade = new FirstTractorUpgrade();
        potatoSpeedUpgrade = new PotatoSpeedUpgrade();
        addAutomaticTractorUpgrade = new AddAutomaticTractorUpgrade();

    }

    public void OnLoad()
    {
        totalPotatoYieldUpgrade.OnLoad();
        firstTractorUpgrade.OnLoad();
        potatoSpeedUpgrade.OnLoad();
        addAutomaticTractorUpgrade.OnLoad();
    }
}