namespace WJA23Godot.Modifiers;

public class AdditiveModifier : IdleModifier
{
    public long addition = 0;

    public override void Apply()
    {
        base.Apply();
        owner.SetAdded(owner.GetAdded() + addition);
    }
    
    public override void AddModifier()
    {
        base.AddModifier();
        if (!owner.HasModifier(this))
        {
            owner.AddAddition(this);
        }
    }
}