using Godot;
using System;

public partial class FarmField : Node2D
{
	[Export]
	private int squareSize = 5;

	private int xCount = 0;
	private int yCount = 0;
	const int CELL_SIZE = 32;
	public Vector2 positionRelative = Vector2.Zero;
	public bool first = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var node in GetChildren())
		{
			if (node is FarmLand farmLand)
			{
				farmLand.Position = new Vector2(xCount*CELL_SIZE, yCount*CELL_SIZE);
				if (xCount == 2 && yCount == 2 && first) FarmFieldMaster.originFarmLand = farmLand.Position;
				farmLand.position = new Pos2D(xCount, yCount);
				farmLand.cost = (int)farmLand.Position.DistanceTo(FarmFieldMaster.originFarmLand);
				xCount++;
				if (xCount > squareSize - 1)
				{
					xCount = 0;
					yCount++;
				}
			}
		}
	}

	public void Expand(Pos2D pos)
	{
		FarmFieldMaster ffm = GetParent<FarmFieldMaster>();
		FarmFieldMaster.Cardinality cardinality = FarmFieldMaster.Cardinality.EAST;
		if (pos.X == squareSize - 1)
		{
			cardinality = FarmFieldMaster.Cardinality.WESTS;

		}else if (pos.X == 0)
		{
			cardinality = FarmFieldMaster.Cardinality.EAST;
		}
		if (pos.X == 0 || pos.X == squareSize - 1)
			ffm.Expand(cardinality, positionRelative);

		if (pos.Y == squareSize - 1)
		{
			cardinality = FarmFieldMaster.Cardinality.NORTH;
		}else if (pos.Y == 0)
		{
			cardinality = FarmFieldMaster.Cardinality.SOUTH;
		}
		if (pos.Y == 0 || pos.Y == squareSize - 1)
			ffm.Expand(cardinality, positionRelative);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
