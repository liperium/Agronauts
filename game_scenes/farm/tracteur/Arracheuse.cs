using Godot;
using System;

public partial class Arracheuse : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += Collided;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Collided(Area2D area)
	{
		if (area is FarmLand farmLand)
		{
			if (farmLand.CurrState == FarmLand.LandState.Ready)
			{
				farmLand.Harvest();
			}
		}
	}
}
