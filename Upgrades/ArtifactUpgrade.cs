using Godot;
using System;
using WJA23Godot.Upgrades;

public partial class ArtifactUpgrade<TModifier> : TieredUpgrade<TModifier>, IArtifact 
    where TModifier :  IdleModifier , new()
{
    public override string GetEffectText()
    {
        return "("+tier+") ";
    }

    /// <summary>
    /// Gets the weight of the artifact representing the odd of it being dropped rolled.
    /// </summary>
    /// <returns>Weight of the artifact</returns>
    public virtual int GetWeight()
    {
        return 0;
    }
    
    public override void UpdateCost()
    {
        cost = 0;
    }

    public override void InitInfo()
    {
        base.InitInfo();
        string rarity = "";
        switch (GetRarity())
        {
            case ArtifactRarity.Common:
                rarity ="KCOMMON";
                break;
            case ArtifactRarity.Uncommon:
                rarity ="KUNCOMMON";
                break;
            case ArtifactRarity.Legendary:
                rarity ="KLEGENDARY";
                break;
        }
        
        string dropChance = GameState.instance.artifacts.GetArtifactDropChancePercentage(this).ToString("0.##");
        
        info.SetAdditionalDescription(rarity + " - "+dropChance+"%");
        
    }

    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Artifact;
    }
    
    /// <summary>
    /// Returns background style depending on artifact rarity
    /// </summary>
    /// <returns>Background style</returns>
    public override StyleBoxFlat GetBgStyle()
    {
        StyleBoxFlat style = null;
        switch (GetRarity())
        {
            case ArtifactRarity.Common:
                style = ResourceLoader.Load<StyleBoxFlat>("res://Upgrades/UpgradeStyles/CommonArtifactStyle.tres");
                break;
        }

        if (style == null)
        {
            GD.PrintErr("ARTIFACT RARITY NULL ON GETBGSTYLE");
        }

        return style;
    }

    /// <summary>
    /// Returns rarity of the artifact. Default is Common.
    /// </summary>
    /// <returns>Rarity of the artifact</returns>
    public virtual ArtifactRarity GetRarity()
    {
        return ArtifactRarity.Common;
    }
}
