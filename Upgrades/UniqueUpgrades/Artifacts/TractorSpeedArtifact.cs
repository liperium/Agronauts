using Godot;
using WJA23Godot.Upgrades;

public class TractorSpeedArtifact : CappedArtifactUpgrade<MultiplierModifier>
{
    public override long GetTierCap()
    {
        return 1;
    }

    public override string GetEffectText()
    {
        return base.GetEffectText() + Mathf.RoundToInt((modifier.multiplier * 100)) + "%";
    }

    public override void InitInfo()
    {
        base.InitInfo();
        info.SetName("KTRUCKSPEEDARTIFACT");
        info.SetDescription("KTRUCKSPEEDARTIFACTDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/moteurextraterrestre.png");
    }

    public override int GetWeight()
    {
        return 1;
    }

    public override ArtifactRarity GetRarity()
    {
        return ArtifactRarity.Legendary;
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.truckSpeed;
    }

    public override void UpdateModifier()
    {
        modifier.multiplier = 1 + tier * 0.5f;
    }
}