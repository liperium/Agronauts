using Godot;
using System;

public partial class Flash : ColorRect
{
	private AnimationTree animationTree;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationTree = GetNode<AnimationTree>("AnimationTree");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void Start()
	{
		animationTree.Set("parameters/conditions/flash", true);
		animationTree.Set("parameters/conditions/idle", false);
	}

	public void Stop()
	{
		animationTree.Set("parameters/conditions/idle", true);
		animationTree.Set("parameters/conditions/flash", false);
	}
}
