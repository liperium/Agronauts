using Godot;
using System;

public partial class WindowModeSelector : MenuButton
{
	private DisplayServer.WindowMode lastNonFullscreenMode = DisplayServer.WindowMode.Maximized;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		long settingsWindowMode = GameState.settings.windowMode;
		foreach (var windowMode in DisplayServer.WindowMode.GetValues(typeof(DisplayServer.WindowMode)))
		{
			GetPopup().AddItem($"K{windowMode.ToString().ToUpper()}");
		}
		ChangeWindowMode(settingsWindowMode);
		GetPopup().IndexPressed += ChangeWindowMode;
	}
	public void ChangeWindowMode(DisplayServer.WindowMode newMode)
	{
		if (newMode != DisplayServer.WindowMode.Fullscreen && newMode != DisplayServer.WindowMode.ExclusiveFullscreen)
		{
			if (lastNonFullscreenMode != DisplayServer.WindowGetMode())
			{
				lastNonFullscreenMode = DisplayServer.WindowGetMode();
			}
			else
			{
				lastNonFullscreenMode = newMode;
			}
		}

		GD.Print($"Changed window mode - {newMode}");
		if (DisplayServer.WindowGetMode() != newMode)
		{
			DisplayServer.WindowSetMode(newMode);
		}
		SaveAll((long)newMode);
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

	public override void _ExitTree()
	{
		base._ExitTree();

		SaveAll((long)DisplayServer.WindowGetMode()); // If window was changed externally it needs to save the last.
	}

	public void SaveAll(long newMode)
	{
		ProjectSettings.SetSetting("display/window/size/mode",newMode);
		GameState.settings.windowMode = newMode;

		GameState.SaveSettings();
	}
	//TODO window size
}
