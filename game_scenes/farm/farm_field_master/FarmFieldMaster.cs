using Godot;
using System;
using System.Collections.Generic;

public partial class FarmFieldMaster : Node2D
{
	//TODO Spawn temp fields when edge of a field is ateined
	private PackedScene FarmField =
		ResourceLoader.Load<PackedScene>("res://game_scenes/farm/farm_field/farm_field.tscn");

    public static int TILE_SIZE = 32;
    public static int TILE_PER_FF = 5;
	const int MAX_SIZE = 33; // HAS TO BE IMPAIR
	public static int centerPos = (MAX_SIZE - 1) / 2;
	public static Vector2 originFarmLand;
    private bool[,] isTaken = new bool[MAX_SIZE,MAX_SIZE];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpawnField(centerPos, centerPos, true);
	}

	public void SpawnField(int x, int y, bool origin = false)
	{
		isTaken[x, y] = true;
		FarmField newChild = FarmField.Instantiate() as FarmField;
		newChild.positionRelative = new Vector2(x, y);
		newChild.first = origin;
		newChild.Position = new Vector2((16 - x)*TILE_SIZE*TILE_PER_FF,(16 - y)*TILE_SIZE*TILE_PER_FF);
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

	public void Expand(Cardinality side, Vector2 posInMaster)
	{
		Vector2 toAdd = posInMaster;
		switch (side)
		{
			case Cardinality.EAST:
				toAdd.X++;
				break;
			case Cardinality.WESTS:
				toAdd.X--;
				break;
			case Cardinality.NORTH:
				toAdd.Y--;
				break;
			case Cardinality.SOUTH:
				toAdd.Y++;
				break;
		}

		if (toAdd.X < 0 || toAdd.X > MAX_SIZE - 1)
		{
			return;
		}
		if (toAdd.Y < 0 || toAdd.Y > MAX_SIZE - 1)
		{
			return;
		}

		if (isTaken[(int)toAdd.X, (int)toAdd.Y])
		{
			return;
		}

		SpawnField((int)toAdd.X, (int)toAdd.Y);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
