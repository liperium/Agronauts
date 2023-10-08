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
    public AutoCookLevel4Upgrade autoCookLevel4Upgrade;
    public FurnaceSpeedUpgrade furnaceSpeedUpgrade;
    public FurnaceBatchSizeUpgrade furnaceBatchSizeUpgrade;
    public TractorSpeedArtifact tractorSpeedArtifact;
    public FurnaceSpeedArtifact furnaceSpeedArtifact;
    public FurnaceTempUpgrade furnaceTempUpgrade;

    public UnlockFurnaceUpgrade unlockFurnaceUpgrade;
    public UnlockArtifactsUpgrade unlockArtifactsUpgrade;
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
        autoCookLevel4Upgrade = new AutoCookLevel4Upgrade();
        furnaceSpeedUpgrade = new FurnaceSpeedUpgrade();
        furnaceBatchSizeUpgrade = new FurnaceBatchSizeUpgrade();

        tractorSpeedArtifact = new TractorSpeedArtifact();
        furnaceSpeedArtifact = new FurnaceSpeedArtifact();
        furnaceTempUpgrade = new FurnaceTempUpgrade();

        unlockFurnaceUpgrade = new UnlockFurnaceUpgrade();
        unlockArtifactsUpgrade = new UnlockArtifactsUpgrade();

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
        autoCookLevel4Upgrade.OnLoad();
        furnaceSpeedUpgrade.OnLoad();
        furnaceBatchSizeUpgrade.OnLoad();

        tractorSpeedArtifact.OnLoad();
        furnaceSpeedArtifact.OnLoad();
        furnaceTempUpgrade.OnLoad();
        
        unlockFurnaceUpgrade.OnLoad();
        unlockArtifactsUpgrade.OnLoad();
    }
}