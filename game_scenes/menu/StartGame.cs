using Godot;
using System;
using WJA23Godot.GameState;

public partial class StartGame : Button
{
	[Export] public PackedScene farmScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Pressed()
	{
		base._Pressed();
		SceneTransition.GoToScene(farmScene, GameScene.Farm);
	}
}
