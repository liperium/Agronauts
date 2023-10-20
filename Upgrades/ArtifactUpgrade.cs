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

        string rarity = "K"+GetRarity().ToString().ToUpper();

        string dropChance = "";
        if (IsMaxed() == false)
        {
            dropChance = GameState.instance.artifacts.GetArtifactDropChancePercentage(this).ToString("0.##");
            dropChance = $" - {dropChance}%";
        }
        
        info.SetAdditionalDescription(rarity + dropChance);
        
    }

    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Artifact;
    }
    
    /// <summary>
    /// Returns background style depending on artifact rarity
    /// </summary>
    /// <returns>Background style</returns>
    public override string GetBgStyle()
    {
        string style = "";
        switch (GetRarity())
        {
            case ArtifactRarity.Common:
                style = "CommonArtifact";
                break;
            case ArtifactRarity.Uncommon:
                style = "UncommonArtifact";
                break;
            case ArtifactRarity.Legendary:
                style = "LegendaryArtifact";
                break;
        }

        if (style == "")
        {
            GD.PrintErr("ARTIFACT RARITY NOT SET ON GETBGSTYLE");
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
