using System;

[Serializable]
public class IdleUpgradeContainer
{
    public TotalPotatoYieldUpgrade totalPotatoYieldUpgrade;

    public void Init()
    {
        totalPotatoYieldUpgrade = new TotalPotatoYieldUpgrade();
    }

    public void OnLoad()
    {
        totalPotatoYieldUpgrade.OnLoad();
    }
}