using Godot;

public partial class PotatoCountLabel : Node
{
    private IdleNumber number;
    public override void _Ready()
    {
        base._Ready();

        number = GameState.instance.numbers.potatoCount;
        
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
