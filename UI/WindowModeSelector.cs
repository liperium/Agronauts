using Godot;
using System;

public partial class WindowModeSelector : MenuButton
{
	private DisplayServer.WindowMode lastNonFullscreenMode = DisplayServer.WindowMode.Maximized;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		double settingsSoundVolume = GameState.settings.windowMode;
		foreach (var windowMode in DisplayServer.WindowMode.GetValues(typeof(DisplayServer.WindowMode)))
		{
			GetPopup().AddItem($"{windowMode.ToString()}");
		}
		ChangeWindowMode((long)settingsSoundVolume);
		GetPopup().IndexPressed += ChangeWindowMode;
	}
	public void ChangeWindowMode(DisplayServer.WindowMode newMode)
	{
		if (DisplayServer.WindowGetMode() == newMode)
		{
			return;
		}

		if (newMode != DisplayServer.WindowMode.Fullscreen && newMode != DisplayServer.WindowMode.ExclusiveFullscreen)
		{
			lastNonFullscreenMode = newMode;
		}
		GD.Print($"Changed window mode - {newMode}");
		DisplayServer.WindowSetMode(newMode);
		ProjectSettings.SetSetting("display/window/size/mode",(long)newMode);
		GameState.settings.windowMode = (long)newMode;
		GameState.SaveSettings();
	}

	public void ChangeWindowMode(long newModeLong)
	{
		ChangeWindowMode((DisplayServer.WindowMode)newModeLong);
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if(@event.IsActionPressed("toggle_fullscreen"))
		{
			if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen || DisplayServer.WindowGetMode() == DisplayServer.WindowMode.ExclusiveFullscreen)
			{
				ChangeWindowMode(lastNonFullscreenMode);
			}
			else
			{
				ChangeWindowMode(DisplayServer.WindowMode.Fullscreen);
			}
		}
	}
}
