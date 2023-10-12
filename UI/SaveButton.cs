using Godot;
using System;

public partial class SaveButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();

        if (GameState.SAVE_ENABLED)
        {
            GameState.instance.SaveToFile();
        }
    }
}
