using Godot;

public partial class CookedPotatoCountLabel : IdleNumberLabel
{
    public override IdleNumber GetIdleNumber()
    {
        return GameState.instance.numbers.cookedPotatoCount;
    }
}