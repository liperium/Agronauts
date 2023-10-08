using Godot;

public partial class CookedPotatoCountLabel : Node
{
    private IdleNumber number;
    public override void _Ready()
    {
        base._Ready();

        number = GameState.instance.numbers.cookedPotatoCount;
        
        number.SetOnValueChanged(ValueChanged);
        
        GetParent<LerpValue>().Init(number.GetValue());
    }

    private void ValueChanged(long newValue)
    {
        GetParent<LerpValue>().SetNewValue(newValue);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        number.ResetOnValueChanged(ValueChanged);
    }
}