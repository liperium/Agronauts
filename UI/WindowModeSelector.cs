using Godot;
using System;

public partial class WindowModeSelector : MenuButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		double settingsSoundVolume = GameState.settings.windowMode;
		foreach (var windowMode in DisplayServer.WindowMode.GetValues(typeof(DisplayServer.WindowMode)))
		{
			GetPopup().AddItem($"K{windowMode.ToString().ToUpper()}");
		}
		ChangeWindowMode((long)settingsSoundVolume);
		GetPopup().IndexPressed += ChangeWindowMode;
	}
	public void ChangeWindowMode(long index)
	{
		if (DisplayServer.WindowGetMode() == (DisplayServer.WindowMode)index)
		{
			return;
		}
		DisplayServer.WindowSetMode((DisplayServer.WindowMode)index);
		GameState.settings.windowMode = index;
		GameState.SaveSettings();
	}
}
