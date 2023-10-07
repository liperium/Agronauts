using Godot;
using System;

public partial class FarmLand : Node2D
{
	//TODO Input
	enum LandState
	{
		Base,
		Planted,
		Ready
	}

	private CompressedTexture2D baseTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/Terre_Labouree.png");

	private CompressedTexture2D plantedTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/Terre_Plante1.png");

	private CompressedTexture2D readyTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/Terre_Pret1.png");

	private TextureButton button;
	private LandState currState = LandState.Base;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		button = GetNode<TextureButton>("TextureButton");
		button.Pressed += Clicked;
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
		button.TextureNormal = plantedTexture;
		currState = LandState.Planted;
	}
	public void Harvest()
	{
		button.TextureNormal = baseTexture;
		currState = LandState.Base;
	}

	public void Grown()
	{
		button.TextureNormal = readyTexture;
		currState = LandState.Ready;
	}
}
