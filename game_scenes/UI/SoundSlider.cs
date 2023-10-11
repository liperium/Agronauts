using Godot;
using System;

public enum SoundCategory
{
	Master = 1,
	Music = 2,
	Sfx = 3
}

public partial class SoundSlider : HSlider
{
	[Export] public SoundCategory category;
	
	private int busID = 0;
	//TODO save volume settings in another file
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		double settingsSoundVolume = 0;
		if (GameState.settings.soundVolumes.ContainsKey(category))
		{
			settingsSoundVolume = GameState.settings.soundVolumes[category];
		}
		
		busID = (int)category;
		
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
