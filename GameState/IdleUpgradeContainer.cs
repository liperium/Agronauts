using System;
using WJA23Godot.GameState;

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
    public FurnacePotatoRecyclerUpgrade furnacePotatoRecyclerUpgrade;
    public FurnaceTempUpgrade furnaceTempUpgrade;
    public TractorSpreadSeedsUpgrade tractorSpreadSeedsUpgrade;
    public TempTractorSpeedUpgrade tempTractorSpeedUpgrade;

    public UnlockFurnaceUpgrade unlockFurnaceUpgrade;
    public UnlockArtifactsUpgrade unlockArtifactsUpgrade;

    public InvasionTimeUpgrade invasionTimeUpgrade;
    
    //Artifacts
    public FurnaceSpeedArtifact furnaceSpeedArtifact;
    public PotatoYieldArtifact potatoYieldArtifact;

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
        furnacePotatoRecyclerUpgrade = new FurnacePotatoRecyclerUpgrade();

        furnaceTempUpgrade = new FurnaceTempUpgrade();
        tempTractorSpeedUpgrade = new TempTractorSpeedUpgrade();
        tractorSpreadSeedsUpgrade = new TractorSpreadSeedsUpgrade();

        unlockFurnaceUpgrade = new UnlockFurnaceUpgrade();
        unlockArtifactsUpgrade = new UnlockArtifactsUpgrade();

        invasionTimeUpgrade = new InvasionTimeUpgrade();
        
        //Artifacts
        furnaceSpeedArtifact = new FurnaceSpeedArtifact();
        potatoYieldArtifact = new PotatoYieldArtifact();
    }

    public void OnLoad()
    {
        //Add before load to calculate % drop chance on load.
        ArtifactContainer artifactContainer = GameState.instance.artifacts;
        artifactContainer.AddArtifact(furnaceSpeedArtifact);
        artifactContainer.AddArtifact(potatoYieldArtifact);
        
        
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
        furnacePotatoRecyclerUpgrade.OnLoad();
        furnaceTempUpgrade.OnLoad();
        tempTractorSpeedUpgrade.OnLoad();
        
        unlockFurnaceUpgrade.OnLoad();
        unlockArtifactsUpgrade.OnLoad();
        tractorSpreadSeedsUpgrade.OnLoad();
        
        invasionTimeUpgrade.OnLoad();
        
        //artifacts
        furnaceSpeedArtifact.OnLoad();
        potatoYieldArtifact.OnLoad();
        

    }
}