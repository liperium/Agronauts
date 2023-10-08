using Godot;
using System;

public partial class QuitButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        
        //save before quit
        if (GameState.SAVE_ENABLED) GameState.instance.SaveToFile();
        GetTree().Quit();
    }
}
