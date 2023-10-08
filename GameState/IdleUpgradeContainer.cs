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
    public AutoCookLevel2Upgrade autoCookLevel2Upgrade;
    public AutoCookLevel3Upgrade autoCookLevel3Upgrade;
    public void Init()
    {
        totalPotatoYieldUpgrade = new TotalPotatoYieldUpgrade();
        firstTractorUpgrade = new FirstTractorUpgrade();
        potatoSpeedUpgrade = new PotatoSpeedUpgrade();
        addAutomaticTractorUpgrade = new AddAutomaticTractorUpgrade();
        tractorSpeedUpgrade = new TractorSpeedUpgrade();
        autoFurnaceUpgrade = new AutoFurnaceUpgrade();
        autoCookLevel2Upgrade = new AutoCookLevel2Upgrade();
        autoCookLevel3Upgrade = new AutoCookLevel3Upgrade();
    }

    public void OnLoad()
    {
        totalPotatoYieldUpgrade.OnLoad();
        firstTractorUpgrade.OnLoad();
        potatoSpeedUpgrade.OnLoad();
        addAutomaticTractorUpgrade.OnLoad();
        tractorSpeedUpgrade.OnLoad();
        autoFurnaceUpgrade.OnLoad();
        autoCookLevel2Upgrade.OnLoad();
        autoCookLevel3Upgrade.OnLoad();
    }
}