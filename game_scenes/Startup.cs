using Godot;
using System;

public partial class Startup : Node
{
	public override void _Ready()
	{
		SceneTransition.MainMenuAndLoadSettings();
	}
}
