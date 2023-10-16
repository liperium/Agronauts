using Godot;
using System;

public partial class MainMenuSettings : VBoxContainer
{

	private BaseButton button = null;
	private Control settings = null;

	private bool opened;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		button = GetNode<Button>("Button");
		settings = GetNode<Control>("PanelContainer");
		settings.Hide();
		button.Pressed += ToggleSettings;
	}

	private void ToggleSettings()
	{
		opened = !opened;
		if (opened)
		{
			settings.Show();
		}
		else
		{
			settings.Hide();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
