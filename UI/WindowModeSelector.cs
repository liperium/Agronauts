using Godot;
using System;
using System.Collections.Generic;

public partial class WindowModeSelector : MenuButton
{
	private DisplayServer.WindowMode lastNonFullscreenMode = DisplayServer.WindowMode.Maximized;

	private Dictionary<int, DisplayServer.WindowMode> translater = new Dictionary<int, DisplayServer.WindowMode>();

	private List<DisplayServer.WindowMode> excludedModes = new List<DisplayServer.WindowMode>()
	{
		DisplayServer.WindowMode.Fullscreen,
		DisplayServer.WindowMode.Minimized,
	};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		long settingsWindowMode = GameState.settings.windowMode;
		int i = 0;
		foreach (DisplayServer.WindowMode windowMode in DisplayServer.WindowMode.GetValues(typeof(DisplayServer.WindowMode)))
		{
			if (excludedModes.Contains(windowMode)) continue;
			translater.Add(i++, windowMode);
			GetPopup().AddItem($"K{windowMode.ToString().ToUpper()}");
		}

		ChangeWindowMode(settingsWindowMode);
		GetPopup().IndexPressed += ChangeWindowMode;
	}
	public void ChangeWindowMode(DisplayServer.WindowMode newMode)
	{

		if (!isFullscreen(newMode))
		{
			lastNonFullscreenMode = DisplayServer.WindowGetMode();
		}

		if (DisplayServer.WindowGetMode() != newMode)
		{
			CheckSaveWindowSize(); // If we change from window, we save the size of it's last time
			DisplayServer.WindowSetMode(newMode);
			if (newMode == DisplayServer.WindowMode.Windowed) SetToLastSavedSize();
		}
		SaveAll((long)newMode);
	}

	public void ChangeWindowMode(long newModeLong)
	{
		ChangeWindowMode(translater[(int)newModeLong]);
	}

	private static bool isFullscreen(DisplayServer.WindowMode windowMode)
	{
		if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen ||
		    DisplayServer.WindowGetMode() == DisplayServer.WindowMode.ExclusiveFullscreen)
		{
			return true;
		}
		return false;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if(@event.IsActionPressed("toggle_fullscreen"))
		{
			if (isFullscreen(DisplayServer.WindowGetMode()))
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
		CheckSaveWindowSize();
		SaveAll((long)DisplayServer.WindowGetMode()); // If window was changed externally it needs to save the last.
	}

	public void SaveAll(long newMode)
	{
		ProjectSettings.SetSetting("display/window/size/mode",newMode);
		GameState.settings.windowMode = newMode;

		GameState.SaveSettings();
	}

	private void CheckSaveWindowSize(bool saveSetting = false)
	{
		if (DisplayServer.WindowMode.Windowed == DisplayServer.WindowGetMode())
		{
			Pos2D size = new Pos2D(DisplayServer.WindowGetSize().X, DisplayServer.WindowGetSize().Y);
			GameState.settings.lastWindowedSize = size;

			Pos2D position = new Pos2D(DisplayServer.WindowGetPosition().X, DisplayServer.WindowGetPosition().Y);
			GameState.settings.lastWindowedPos = position;
			if (saveSetting)GameState.SaveSettings();
		}
	}

	private void SetToLastSavedSize()
	{
		DisplayServer.WindowSetSize(new Vector2I(GameState.settings.lastWindowedSize.X, GameState.settings.lastWindowedSize.Y));
		DisplayServer.WindowSetPosition(new Vector2I(GameState.settings.lastWindowedPos.X, GameState.settings.lastWindowedPos.Y));
	}
}
