using Godot;
using System;

public partial class ObjectSpawner : Node2D
{
	static ObjectSpawner currSpawner;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currSpawner = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void Spawn(Node2D node, Vector2 position)
	{
		currSpawner.GetTree().CurrentScene.AddChild(node);
		node.Position = position;	
	}
}
