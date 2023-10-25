using Godot;
using System;

public partial class QuitButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();

        if (GameState.SAVE_ENABLED && GameState.settings.saveOnQuit) GameState.instance.SaveToFile();
        GetTree().Quit();
    }
}
