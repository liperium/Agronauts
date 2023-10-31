using Godot;

public partial class FightLoseScreen : Control
{
    [Export] public RichTextLabel fightResultDescLabel;

    public override void _Ready()
    {
        base._Ready();

        long cookedPotatoAmountLost = FightManager.instance.cachedHP;
        fightResultDescLabel.Text = string.Format(Tr("KLOSEDESC"), cookedPotatoAmountLost);

        FightManager.instance.cachedHP = 0;
    }
}