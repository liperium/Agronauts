using Godot;

public partial class PotatoCountLabel : IdleNumberLabel
{
    public override IdleNumber GetIdleNumber()
    {
        return GameState.instance.numbers.potatoCount;
    }
}
