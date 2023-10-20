using Godot;
using WJA23Godot.Modifiers;
using WJA23Godot.Upgrades;

public class CritChanceArtifact : CappedArtifactUpgrade<AdditiveModifier>
{
    public override long GetTierCap()
    {
        return 20;
    }

    public override string GetEffectText()
    {
        return base.GetEffectText() + modifier.addition+"%";
    }

    public override void InitInfo()
    {
        base.InitInfo();
        info.SetName("KCRITCHANCEARTIFACT");
        info.SetDescription("KCRITCHANCEARTIFACTDESC");
        info.SetImagePath(InfoUpgrade.defaultImagePath);
    }

    public override int GetWeight()
    {
        return 25;
    }

    public override ArtifactRarity GetRarity()
    {
        return ArtifactRarity.Uncommon;
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.critChance;
    }

    public override void UpdateModifier()
    {
        modifier.addition = tier;
    }
}