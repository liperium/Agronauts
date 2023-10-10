using Godot;

public partial class PotatoCountLabel : IdleNumberLabel
{
    protected override IdleNumber GetIdleNumber()
    {
        return GameState.instance.numbers.potatoCount;
    }
}
