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
	public void ChangeWindowMode(long newModeInt)
	{
		DisplayServer.WindowMode newWindowMode = (DisplayServer.WindowMode)newModeInt;
		if (DisplayServer.WindowGetMode() == newWindowMode)
		{
			return;
		}
		GD.Print($"Changed window mode - {newWindowMode}");
		DisplayServer.WindowSetMode(newWindowMode);
		ProjectSettings.SetSetting("display/window/size/mode",newModeInt);
		GameState.settings.windowMode = newModeInt;
		GameState.SaveSettings();
	}
}
