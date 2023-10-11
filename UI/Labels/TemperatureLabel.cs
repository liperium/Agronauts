using Godot;
using System;

public partial class TemperatureLabel : IdleNumberLabel
{
    public override IdleNumber GetIdleNumber()
    {
        return GameState.instance.numbers.potatoTemperature;
    }
}
