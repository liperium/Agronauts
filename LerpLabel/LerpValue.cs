using Godot;
using System;

public partial class LerpValue : Label
{
    private long displayValue;

    private long targetValue;

    private bool shouldUpdate = false;

    [Export] 
    public float lerpSpeed = 1.0f;
    public void SetNewValue(long value)
    {
        targetValue = value;

        shouldUpdate = true;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (shouldUpdate)
        {
            
            
            shouldUpdate = displayValue != targetValue;
        }
    }
}
