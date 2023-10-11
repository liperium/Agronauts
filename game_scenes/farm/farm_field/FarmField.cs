using Godot;
using System;

public partial class FarmField : Node2D
{
	const int MAX_SIZE = 5;

	private int xCount = 0;
	private int yCount = 0;
	const int CELL_SIZE = 32;
	public Pos2D positionRelative = new (0,0);
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
					farmLand.FirstShow();
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
				farmLand.ChangeCost(
					(long)Math.Pow((float)farmLand.GlobalPosition.DistanceTo(FarmFieldMaster.originFarmLand)/12f,2.5f
						)
					);
			}
		}


        GameState.instance.upgrades.addAutomaticTractorUpgrade.AddField(this);
    }




    public void LandBought(Pos2D pos2D, bool addToHistory = true)
    {
	    isBought[pos2D.X, pos2D.Y] = true;
	    
	    //add to saved history
	    if (addToHistory)
	    {
		    GameState.instance.savedFields.AddBoughtFieldToHistory(new SavedField()
		    {
			    field = positionRelative,
			    tile = pos2D
		    });   
	    }

	    // Show all other sides
	    // Inside first
	    Pos2D[] variations = { new (0, 1), new (1, 0), new (-1, 0), new (0, -1) };
	    foreach (var possDir in variations)
	    {
		    Pos2D posNei = new Pos2D(pos2D.X,pos2D.Y)+possDir;
		    Pos2D posNextNei = new Pos2D(pos2D.X,pos2D.Y) - new Pos2D(2,2);
		    Pos2D otherFarmFieldPos = new Pos2D(positionRelative.X,positionRelative.Y);


		    //Over border check
		    bool callParent = false;
			//ICI on sort
			if (posNei.X < 0)
			{
				posNextNei.X = -posNextNei.X;
				otherFarmFieldPos.X--;
				callParent = true;
			}else if (posNei.X > MAX_SIZE - 1)
			{
				posNextNei.X = -posNextNei.X;
				otherFarmFieldPos.X++;
				callParent = true;
			}else if (posNei.Y < 0)
			{
				posNextNei.Y = -posNextNei.Y;
				otherFarmFieldPos.Y--;
				callParent = true;
			}else if (posNei.Y > MAX_SIZE - 1)
			{
				posNextNei.Y = -posNextNei.Y;
				otherFarmFieldPos.Y++;
				callParent = true;
			}
			else
			{
				GetNode<FarmLand>($"{posNei.X}-{posNei.Y}").FirstShow();
			}
			posNextNei += new Pos2D(2,2);
			posNextNei.X = Math.Abs(posNextNei.X);
			posNextNei.Y = Math.Abs(posNextNei.Y);
			if (callParent)
			{
				GetParent<FarmFieldMaster>().BuyOrExpand(otherFarmFieldPos, posNextNei);
			}

	    }

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
