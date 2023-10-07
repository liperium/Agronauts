using Godot;
using System;

public partial class LerpValue : Label
{
    private long displayValue;

    private long targetValue;

    private bool shouldUpdate = false;

    [Export] 
    public float lerpSpeed = 1.0f;

    public override void _Ready()
    {
        base._Ready();
        
        //test
        SetNewValue(350);
    }

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
            long toAdd = (long)(delta * lerpSpeed);
            toAdd = Math.Min(1, toAdd);
            displayValue += toAdd;
            
            shouldUpdate = displayValue != targetValue;

            Text = displayValue.FormattedNumber();
        }
    }
}
