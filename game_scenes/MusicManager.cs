using Godot;
using System;

public partial class MusicManager : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AudioServer.SetBusVolumeDb(4, 0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}