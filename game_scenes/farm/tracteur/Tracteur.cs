using Godot;
using System;

public partial class Tracteur : CharacterBody2D
{
	private float speed = 140.0f;
	private float rotationSpeed = 4.0f;
	private Vector2 velocity = Vector2.Zero;

	private float rotationForce = 0.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionPressed("tracteur_front") || Input.IsActionPressed("tracteur_back"))
		{
			float front_back = Input.GetActionStrength("tracteur_front") - Input.GetActionStrength("tracteur_back");
			velocity = front_back * speed * Transform.Y * (float)delta;
			rotationForce = (Input.GetActionStrength("tracteur_right")-Input.GetActionStrength("tracteur_left")) * rotationSpeed * (float)delta;
			if (front_back > 0)
			{
				Rotate(rotationForce);
			}
			else
			{
				Rotate(-rotationForce);
			}
			
			MoveAndCollide(velocity);
		}
	}
}
