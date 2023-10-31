using System;
using System.Collections.Generic;
using Godot;
using static Godot.DisplayServer;

public partial class WindowModeSelector : MenuButton
{
    private const WindowMode defaultMode = WindowMode.Maximized;

    private readonly List<WindowMode> excludedModes = new()
    {
        WindowMode.Fullscreen,
        WindowMode.Minimized
    };

    private WindowMode lastNonFullscreenMode = defaultMode;

    private readonly Dictionary<int, WindowMode> translateFromPopups = new();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var settingsWindowMode = GameState.settings.windowMode;
        var y = 0;
        foreach (WindowMode windowMode in Enum.GetValues(typeof(WindowMode)))
        {
            if (excludedModes.Contains(windowMode))
                continue;
            translateFromPopups.Add(y, windowMode);
            GetPopup().AddItem($"K{windowMode.ToString().ToUpper()}");
            y++;
        }

        ChangeWindowMode((WindowMode)settingsWindowMode);
        GetPopup().IndexPressed += ChangeWindowMode;
    }

    public void ChangeWindowMode(WindowMode newMode)
    {
        if (!IsFullscreen(newMode)) lastNonFullscreenMode = WindowGetMode();

        if (WindowGetMode() != newMode)
        {
            CheckSaveWindowSize(); // If we change from window, we save the size of it's last time
            WindowSetMode(newMode);
            if (newMode == WindowMode.Windowed) SetToLastSavedSize();
        }

        SaveAll(newMode);
    }

    public void ChangeWindowMode(long modeFromPopup)
    {
        ChangeWindowMode(translateFromPopups[(int)modeFromPopup]);
    }

    private static bool IsFullscreen(WindowMode windowMode)
    {
        if (WindowGetMode() == WindowMode.Fullscreen ||
            WindowGetMode() == WindowMode.ExclusiveFullscreen)
            return true;
        return false;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("toggle_fullscreen"))
        {
            if (IsFullscreen(WindowGetMode()))
                ChangeWindowMode(lastNonFullscreenMode);
            else
                ChangeWindowMode(WindowMode.Fullscreen);
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        CheckSaveWindowSize();
        //TODO save on quit with correct binding
        SaveAll(WindowGetMode()); // If window was changed externally it needs to save the last.
    }

    public void SaveAll(WindowMode newMode)
    {
        ProjectSettings.SetSetting("display/window/size/mode", (long)newMode);
        GameState.settings.windowMode = (long)newMode;

        GameState.SaveSettings();
    }

    private void CheckSaveWindowSize(bool saveSetting = false)
    {
        if (WindowMode.Windowed == WindowGetMode())
        {
            var size = new Pos2D(WindowGetSize().X, WindowGetSize().Y);
            GameState.settings.lastWindowedSize = size;

            var position = new Pos2D(WindowGetPosition().X, WindowGetPosition().Y);
            GameState.settings.lastWindowedPos = position;
            if (saveSetting) GameState.SaveSettings();
        }
    }

    private void SetToLastSavedSize()
    {
        WindowSetSize(new Vector2I(GameState.settings.lastWindowedSize.X,
            GameState.settings.lastWindowedSize.Y));
        WindowSetPosition(new Vector2I(GameState.settings.lastWindowedPos.X,
            GameState.settings.lastWindowedPos.Y));
    }
}