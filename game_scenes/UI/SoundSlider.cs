using Godot;
using System;

public partial class SoundSlider : HSlider
{
	[Export] public int busID;
	//TODO save volume settings in another file
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		double volume = Mathf.LinearToDb(Value);
		AudioServer.SetBusVolumeDb(busID, (int)(volume));
	}

	public override void _ValueChanged(double newValue)
	{
		double volume = Mathf.LinearToDb(newValue);
		AudioServer.SetBusVolumeDb(busID, (int)(volume));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
