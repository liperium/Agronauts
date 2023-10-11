using Godot;
using System;

public enum SoundCategory
{
	Master,
	Music,
	Sfx
}

public partial class SoundSlider : HSlider
{
	[Export] public SoundCategory category;
	
	[Export] public int busID;
	//TODO save volume settings in another file
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		double settingsSoundVolume = GameState.settings.soundVolumes[category];
		
		double volume = Mathf.LinearToDb(settingsSoundVolume);
		AudioServer.SetBusVolumeDb(busID, (int)(volume));

		Value = settingsSoundVolume;
	}

	public override void _ValueChanged(double newValue)
	{
		double volume = Mathf.LinearToDb(newValue);
		AudioServer.SetBusVolumeDb(busID, (int)(volume));

		GameState.settings.soundVolumes[category] = newValue;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
