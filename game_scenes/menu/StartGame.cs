using Godot;
using System;

public partial class StartGame : Button
{
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
		var scene = ResourceLoader.Load<PackedScene>("res://game_scenes/farm/farm.tscn");
		GetTree().ChangeSceneToPacked(scene);
	}
}
