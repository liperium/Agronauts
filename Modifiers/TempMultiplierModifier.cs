using Godot;
using System;
[Serializable]
public partial class TempMultiplierModifier : IdleModifier
{
    float multiplier = 1.0f;
    float time;

    public override void Apply()
    {
        base.Apply();
    }
}
