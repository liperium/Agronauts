using Godot;
using System;

public partial class SaveManager : Node
{
    public override void _Ready()
    {
        base._Ready();
        
        GameState state = GameState.instance;

        GameState.LoadSettings();
    }
}
