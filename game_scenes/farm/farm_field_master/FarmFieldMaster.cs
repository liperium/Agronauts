using Godot;
using System;
using System.Collections.Generic;

public partial class FarmFieldMaster : Node2D
{
	private PackedScene FarmField;

    public static int TILE_SIZE = 32;
    public static int TILE_PER_FF = 5;
	const int MAX_SIZE = 33; // HAS TO BE IMPAIR
	public static int centerPos = (MAX_SIZE - 1) / 2;
	public static Vector2 originFarmLand;
    private bool[,] isTaken = new bool[MAX_SIZE,MAX_SIZE];

    public static float farmTime = 3.0f;
    public static IdleAction OnFarmTimeChange;

	// Called when the node enters the scene tree for the first time.
	public FarmFieldMaster()
	{
		OnFarmTimeChange = new IdleAction();
	}

	public override void _Ready()
	{
		FarmField  =
		ResourceLoader.Load<PackedScene>("res://game_scenes/farm/farm_field/farm_field.tscn");
		SpawnField(centerPos, centerPos, true);

		UpdateFarmTime(GameState.instance.numbers.potatoGrowSpeed.GetValue());
		GameState.instance.numbers.potatoGrowSpeed.SetOnValueChanged(UpdateFarmTime);

		ProcessHistory();
	}

	private void UpdateFarmTime(long speed)
	{
		farmTime = 3.0f / speed;
		OnFarmTimeChange.Invoke();
	}

	public void SpawnField(int x, int y, bool origin = false)
	{
		isTaken[x, y] = true;
		FarmField newChild = FarmField.Instantiate() as FarmField;
		newChild.positionRelative = new Pos2D(x, y);
		newChild.first = origin;
		newChild.Name = $"{x}-{y}";
		newChild.Position = new Vector2(-(16 - x)*TILE_SIZE*TILE_PER_FF,-(16 - y)*TILE_SIZE*TILE_PER_FF);
		AddChild(newChild);
		GameState.instance.numbers.numberOfTilesUnlocked.IncreaseValue(1);
	}
	public enum Cardinality
	{
		NORTH,
		SOUTH,
		EAST,
		WESTS
	}

	public void BuyOrExpand(Pos2D targetField, Pos2D toBuy)
	{

		if (targetField.X < 0 || targetField.X > MAX_SIZE - 1)
		{
			return;
		}
		if (targetField.Y < 0 || targetField.Y > MAX_SIZE - 1)
		{
			return;
		}

		if (!isTaken[targetField.X, targetField.Y])
		{
			SpawnField(targetField.X, targetField.Y);
		}
		// Unlock la tile
		GetNode<FarmField>($"{targetField.X}-{targetField.Y}").GetNode<FarmLand>($"{toBuy.X}-{toBuy.Y}").FirstShow();

	}

	public FarmLand GetFarmTile(Pos2D field, Pos2D tile)
	{
		return GetNode($"{field.X}-{field.Y}").GetNode<FarmLand>($"{tile.X}-{tile.Y}");
	}

	private void ProcessHistory()
	{
		GameState.instance.upgrades.addAutomaticTractorUpgrade.ResetFarmFieldList();
		
		GameState.instance.numbers.numberOfTilesUnlocked.SetValue(0);
		foreach (SavedField savedField in GameState.instance.savedFields.boughtFieldsHistory)
		{
			FarmLand land = GetFarmTile(savedField.field, savedField.tile);
			if (land != null)
			{
				land.Bought(false, false);
				land.Laboure();
			}
		}
		
		//first tractor
		if (GameState.instance.upgrades.firstTractorUpgrade.IsAcquired())
		{
			GameState.instance.upgrades.firstTractorUpgrade.SpawnTractor();
		}
		
		//tractors
		GameState.instance.upgrades.addAutomaticTractorUpgrade.SpawnAllTractors();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
