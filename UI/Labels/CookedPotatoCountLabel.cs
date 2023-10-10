using Godot;

public partial class CookedPotatoCountLabel : IdleNumberLabel
{
    protected override IdleNumber GetIdleNumber()
    {
        return GameState.instance.numbers.cookedPotatoCount;
    }
}