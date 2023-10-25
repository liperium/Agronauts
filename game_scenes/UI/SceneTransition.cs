using Godot;
using System;

public partial class SceneTransition : CanvasLayer
{
	private AnimationPlayer animationPlayer = null;

	private const string AnimName = "dissolve";
	private static SceneTransition instance = null;

	private PackedScene goToScene = null; // What scene is it going to
	private bool active = false; // Is it transitioning
	private bool loadSettings = false; // Does it load the settings

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
			LoadAndAnimatoToScene();
		}
		else if(animname != "RESET")
		{
			animationPlayer.Play("RESET");
		}
	}

	private void LoadAndAnimatoToScene()
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

	public void StartChangeScene(PackedScene scene)
	{
		goToScene = scene;
		animationPlayer.Play(AnimName);
		active = true;
	}

	public static void MainMenuAndLoadSettings()
	{
		PackedScene mainMenu = ResourceLoader.Load<PackedScene>("res://game_scenes/menu/menu.tscn");
		instance.loadSettings = true;
		instance.goToScene = mainMenu;
		instance.LoadAndAnimatoToScene();
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
