using Godot;
using WJA23Godot.Upgrades;

public partial class FurnaceSpeedArtifact : ArtifactUpgrade<MultiplierModifier>
{
    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.furnaceSpeed;
    }

    public override void UpdateModifier()
    {
        modifier.multiplier = 1 +  0.1f * tier;
    }

    public override void InitInfo()
    {
        base.InitInfo();
        info.SetName("KFURNACESPEEDARTIFACT");
        info.SetDescription("KFURNACESPEEDARTIFACTDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/CarburantInterstellaire.png");
    }
    
    public override int GetWeight()
    {
        return 100;
    }
    
    public override string GetEffectText()
    {
        return base.GetEffectText() + Mathf.RoundToInt((modifier.multiplier * 100)) + "%";
    }
    
    public override ArtifactRarity GetRarity()
    {
        return ArtifactRarity.Common;
    }
  
}
