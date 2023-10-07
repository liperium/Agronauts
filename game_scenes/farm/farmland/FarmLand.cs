using Godot;
using System;

public partial class FarmLand : Area2D
{
	public enum LandState
	{
		Base,
		Planted,
		Ready
	}

	public static CompressedTexture2D BaseTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/terre_laboure.png");

	public static CompressedTexture2D PlantedTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/terre_plante1.png");

	public static CompressedTexture2D ReadyTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/terre_pret1.png");

	private TextureButton button;
	private Timer growthTimer;
	private LandState currState = LandState.Base;

	public LandState CurrState => currState;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		button = GetNode<TextureButton>("TextureButton");
		button.Pressed += Clicked;
		growthTimer = GetNode<Timer>("Timer");
		growthTimer.OneShot = true;

		//TODO link with global timer
		growthTimer.WaitTime = 3.0;
		growthTimer.Timeout += Grown;
	}

	public void Clicked()
	{
		GD.Print("State "+currState);
		switch (currState)
		{
			case LandState.Base:
				Plant();
				break;
			case LandState.Planted: break;
			case LandState.Ready:
				Harvest();
				break;
		}
	}

	public void Plant()
	{
		growthTimer.Start();
		button.TextureNormal = PlantedTexture;
		currState = LandState.Planted;
	}
	public void Harvest()
	{
		button.TextureNormal = BaseTexture;
		currState = LandState.Base;
	}

	public void Grown()
	{
		button.TextureNormal = ReadyTexture;
		currState = LandState.Ready;
	}
}
