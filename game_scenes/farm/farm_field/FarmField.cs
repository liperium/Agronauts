using Godot;
using System;

public partial class FarmField : Node2D
{
	const int MAX_SIZE = 5;

	private int xCount = 0;
	private int yCount = 0;
	const int CELL_SIZE = 32;
	public Vector2 positionRelative = Vector2.Zero;
	public bool first = false;
	private bool[,] isBought = new bool[MAX_SIZE,MAX_SIZE];


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var node in GetChildren())
		{
			if (node is FarmLand farmLand)
			{
				farmLand.Position = new Vector2(xCount*CELL_SIZE, yCount*CELL_SIZE);
				if (xCount == 2 && yCount == 2 && first)
				{
					farmLand.Show();
					FarmFieldMaster.originFarmLand = farmLand.GlobalPosition;
				}
				farmLand.position = new Pos2D(xCount, yCount);
				farmLand.Name = $"{xCount}-{yCount}";

				xCount++;
				if (xCount > MAX_SIZE - 1)
				{
					xCount = 0;
					yCount++;
				}
			}
		}

		foreach (var node in GetChildren())
		{
			if (node is FarmLand farmLand)
			{
				farmLand.ChangeCost((int)farmLand.GlobalPosition.DistanceTo(FarmFieldMaster.originFarmLand));
			}
		}


        GameState.instance.upgrades.addAutomaticTractorUpgrade.AddField(this);
    }



    public void Expand(Pos2D pos)
	{
		FarmFieldMaster ffm = GetParent<FarmFieldMaster>();
		FarmFieldMaster.Cardinality cardinality = FarmFieldMaster.Cardinality.EAST;
		if (pos.X == MAX_SIZE - 1)
		{
			cardinality = FarmFieldMaster.Cardinality.WESTS;

		}else if (pos.X == 0)
		{
			cardinality = FarmFieldMaster.Cardinality.EAST;
		}
		if (pos.X == 0 || pos.X == MAX_SIZE - 1)
			ffm.Expand(cardinality, positionRelative);

		if (pos.Y == MAX_SIZE - 1)
		{
			cardinality = FarmFieldMaster.Cardinality.NORTH;
		}else if (pos.Y == 0)
		{
			cardinality = FarmFieldMaster.Cardinality.SOUTH;
		}
		if (pos.Y == 0 || pos.Y == MAX_SIZE - 1)
			ffm.Expand(cardinality, positionRelative);
	}

    public void LandBought(Pos2D pos2D)
    {
	    isBought[pos2D.X, pos2D.Y] = true;
	    // Show all other sides
	    // Inside first
	    Pos2D[] variations = { new (0, 1), new (1, 0), new (-1, 0), new (0, -1) };
	    foreach (var possDir in variations)
	    {
		    Pos2D posNei = pos2D+possDir;

		    //Over border check
		    if (!(posNei.X < 0 || posNei.Y < 0 || posNei.X > MAX_SIZE - 1 || posNei.Y > MAX_SIZE - 1))
		    {
			    GetNode<FarmLand>($"{posNei.X}-{posNei.Y}").Show();
		    }
		    else
		    {

		    }
	    }

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
