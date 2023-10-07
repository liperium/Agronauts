using Godot;
using System;

public partial class PotatoCountLabel : Node
{
    private IdleNumber number;
    public override void _Ready()
    {
        base._Ready();

        number = GameState.instance.numbers.potatoCount;
        
        number.SetOnValueChanged(ValueChanged);
        
        GetParent<LerpValue>().Init(42069);
    }

    private void ValueChanged(long newValue)
    {
        GetParent<LerpValue>().SetNewValue(newValue);
    }
}
