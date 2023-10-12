using Godot;
using System;

public enum SoundCategory
{
	GameMaster = 0,
	PlayerMaster = 1,
	PlayerMusic = 2,
	PlayerSfx = 3,
	GameMusic = 4,
	GameSfx = 5,
	CombatMusic = 6,
}

public partial class SoundSlider : HSlider
{
	[Export] public SoundCategory category;
	
	private int busID = 0;
	//TODO save volume settings in another file
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		double settingsSoundVolume = GameState.settings.soundVolumes[category];
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
		GameState.SaveSettings();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
