using Godot;
using System;

public partial class SceneTransition : CanvasLayer
{
	private AnimationPlayer animationPlayer = null;

	private const string AnimName = "dissolve";
	private static SceneTransition instance = null;

	private PackedScene goToScene = null;

	private bool active = false;

	private static bool loadSettings = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instance = this;
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.AnimationFinished += AnimationFinished;
	}

	private void AnimationFinished(StringName animname)
	{
		if (animname.Equals(AnimName) && active)
		{
			if (loadSettings)
			{
				GameState state = GameState.instance; // Loads the save
				GameState.LoadSettings();
				loadSettings = false;
			}
			GetTree().ChangeSceneToPacked(goToScene);
			animationPlayer.PlayBackwards(AnimName);
			active = false;

		}
		else if(animname != "RESET")
		{
			animationPlayer.Play("RESET");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void StartChangeScene(PackedScene scene)
	{
		goToScene = scene;
		animationPlayer.Play(AnimName);
		active = true;
	}

	public static void MainMenuAndLoadSettings()
	{
		PackedScene mainMenu = ResourceLoader.Load<PackedScene>("res://game_scenes/menu/menu.tscn");
		loadSettings = true;

		instance.StartChangeScene(mainMenu);
	}

	public static void GoToScene(PackedScene scene)
	{
		if (instance == null)
		{
			GD.PrintErr("No SceneTransition instance");
			return;
		}
		instance.StartChangeScene(scene);
	}
}
