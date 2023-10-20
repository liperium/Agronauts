namespace WJA23Godot.Modifiers;

public class AdditiveModifier : IdleModifier
{
    public long addition = 0;

    public override void Apply()
    {
        base.Apply();
        owner.SetAdded(owner.GetAdded() + addition);
    }
    
}