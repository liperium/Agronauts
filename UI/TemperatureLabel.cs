using Godot;
using System;

public partial class TemperatureLabel : Node2D
{
    private IdleNumber number;
    public override void _Ready()
    {
        base._Ready();

        number = GameState.instance.numbers.potatoTemp;
        
        number.SetOnValueChanged(ValueChanged);
        
        GetParent<LerpValue>().Init(number.GetValue());
    }

    private void ValueChanged(long newValue)
    {
        GetParent<LerpValue>().SetNewValue(newValue);
    }
}
