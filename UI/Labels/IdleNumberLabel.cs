using Godot;

public partial class IdleNumberLabel : Node
{
    protected IdleNumber number;
    
    public override void _Ready()
    {
        base._Ready();

        number = GetIdleNumber();

        if (number == null)
        {
            GD.PrintErr($"No idle number set for Idle Number label: {GetType()}. Please override GetIdleNumber and provide a number");
            QueueFree();
            return;
        }
        
        number.SetOnValueChanged(ValueChanged);
        
        GetParent<LerpValue>().Init(number.GetValue());
    }

    public virtual IdleNumber GetIdleNumber()
    {
        return null;
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