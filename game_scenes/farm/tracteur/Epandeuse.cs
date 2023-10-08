using Godot;
using System;


public partial class Epandeuse:Area2D
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
            if (farmLand.CurrState == FarmLand.LandState.Laboure)
            {
                farmLand.Plant();
            }
        }
    }

    public void SetCollision(bool isEnabled)
    {
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = !isEnabled;
    }
}