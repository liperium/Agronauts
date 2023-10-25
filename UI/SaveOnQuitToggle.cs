using Godot;
using System;

public partial class SaveOnQuitToggle : CheckBox
{
	public override void _Ready()
	{
		ButtonPressed = GameState.settings.saveOnQuit;
	}
	public override void _Toggled(bool buttonPressed)
	{
		GameState.settings.saveOnQuit = buttonPressed;
		GameState.SaveSettings();
	}
}
