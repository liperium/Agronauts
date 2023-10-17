using Godot;
using System;using WJA23Godot.Upgrades;

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
    /// <returns>the weight of the artifact</returns>
    public int GetWeight()
    {
        return 0;
    }
}
