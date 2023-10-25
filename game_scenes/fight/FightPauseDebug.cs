using Godot;

public partial class FightPauseDebug : RichTextLabel
{
    public override void _Ready()
    {
        base._Ready();
        FightManager.instance.OnSetPaused += OnPausedChanged;
    }

    private void OnPausedChanged(bool isPaused)
    {
        Text = isPaused ? "[center]PAUSED[/center]" : "";
    }
}
