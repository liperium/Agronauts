using Godot;
using System;

public partial class TemperatureLabel : IdleNumberLabel
{
    protected override IdleNumber GetIdleNumber()
    {
        return GameState.instance.numbers.potatoTemperature;
    }
}
