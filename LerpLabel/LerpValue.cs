using Godot;
using System;

public partial class LerpValue : Label
{
    private long displayValue;

    private long targetValue;

    private bool shouldUpdate = false;

    [Export] 
    public float lerpSpeed = 1.0f;

    private long displayDelta;

    public override void _Ready()
    {
        base._Ready();
        
        //test
        SetNewValue(350);
    }

    public void SetNewValue(long value)
    {
        targetValue = value;

        displayDelta = targetValue - displayValue;

        shouldUpdate = true;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (shouldUpdate)
        {
            long toAdd = (long)(displayDelta * delta * lerpSpeed);
            toAdd = Math.Max(1, toAdd);
            
            displayValue += toAdd;
            displayValue = Math.Min(displayValue, targetValue);

            shouldUpdate = displayValue != targetValue;

            Text = displayValue.FormattedNumber();
        }
    }
}
