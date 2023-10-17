using Godot;
using System;

public partial class ArtifactUpgrade<TModifier> : TieredUpgrade<TModifier> where TModifier : IdleModifier , new()
{
    public override string GetEffectText()
    {
        return "("+tier+") ";
    }
}
