using Godot;
using System;

public partial class WinLoader : Node2D
{
    [Export] public PackedScene winScene;
    public override void _Ready()
    {
        base._Ready();
        GameState.instance.SetOnWin(OnWin);
    }

    private void OnWin()
    {
        if (winScene != null)
        {
           // SceneTransition.GoToScene(winScene);
        }
    }
}
