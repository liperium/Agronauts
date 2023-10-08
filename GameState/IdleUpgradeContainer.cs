using System;

[Serializable]
public class IdleUpgradeContainer
{
    public TotalPotatoYieldUpgrade totalPotatoYieldUpgrade;
    public FirstTractorUpgrade firstTractorUpgrade;
    public PotatoSpeedUpgrade potatoSpeedUpgrade;
    public AddAutomaticTractorUpgrade addAutomaticTractorUpgrade;
    public TractorSpeedUpgrade tractorSpeedUpgrade;
    public AutoFurnaceUpgrade autoFurnaceUpgrade;
    public void Init()
    {
        totalPotatoYieldUpgrade = new TotalPotatoYieldUpgrade();
        firstTractorUpgrade = new FirstTractorUpgrade();
        potatoSpeedUpgrade = new PotatoSpeedUpgrade();
        addAutomaticTractorUpgrade = new AddAutomaticTractorUpgrade();
        tractorSpeedUpgrade = new TractorSpeedUpgrade();
        autoFurnaceUpgrade = new AutoFurnaceUpgrade();
    }

    public void OnLoad()
    {
        totalPotatoYieldUpgrade.OnLoad();
        firstTractorUpgrade.OnLoad();
        potatoSpeedUpgrade.OnLoad();
        addAutomaticTractorUpgrade.OnLoad();
        tractorSpeedUpgrade.OnLoad();
        autoFurnaceUpgrade.OnLoad();
    }
}