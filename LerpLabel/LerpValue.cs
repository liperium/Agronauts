using Godot;
using System;

public partial class LerpValue : RichTextLabel
{
    private long displayValue;

    private long targetValue;

    private bool shouldUpdate;

    [Export] 
    public float lerpSpeed = 1.0f;

    private long displayDelta;

    public void Init(long initialValue)
    {
        displayValue = initialValue;
        targetValue = initialValue;
        Text = initialValue.FormattedNumber();
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
            int direction = Math.Sign(displayDelta);
            
            long toAdd = (long)(displayDelta * delta * lerpSpeed);
            if (toAdd == 0) toAdd = direction;
            
            displayValue += toAdd;

            if (Math.Sign(targetValue - displayValue) * direction == -1)
            {
                displayValue = targetValue;
            }

            shouldUpdate = displayValue != targetValue;

            Text = displayValue.FormattedNumber();
        }
    }
}
