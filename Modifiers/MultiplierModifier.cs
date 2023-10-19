using Godot;
using System;
[Serializable]
public partial class MultiplierModifier : IdleModifier
{
    public float multiplier = 1.0f;

    public override void Apply()
    {
        base.Apply();
        owner.SetMultiplier(owner.GetMultiplier() * multiplier);
    }

    public override void AddModifier()
    {
        base.AddModifier();
        if (!owner.HasModifier(this))
        {
            owner.AddMultiplier(this);
        }
    }
}
