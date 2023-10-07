using System;

[Serializable]
public class IdleUpgradeContainer
{
    public TotalPotatoYieldUpgrade totalPotatoYieldUpgrade;
    public FirstTractorUpgrade firstTractorUpgrade;

    public void Init()
    {
        totalPotatoYieldUpgrade = new TotalPotatoYieldUpgrade();
        firstTractorUpgrade = new FirstTractorUpgrade();
    }

    public void OnLoad()
    {
        totalPotatoYieldUpgrade.OnLoad();
        firstTractorUpgrade.OnLoad();
    }
}