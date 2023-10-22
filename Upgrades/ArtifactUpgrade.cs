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
    /// Returns background style variation name depending on artifact rarity
    /// </summary>
    /// <returns>Background style variation name</returns>
    public override string GetBgStyle()
    {
        return $"{GetRarity().ToString()}Artifact";
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
