using Godot;
using System;

public partial class SaveButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        
        GameState.instance.SaveToFile();
    }
}
