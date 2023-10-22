using Godot;
using System;
using System.Collections.Generic;

public partial class ZoomBlock : Control
{
	private static int nbOfBlockers = 0;
	private static int blockingRn = 0;
	public static ZoomBlock instance = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instance = this;
		Spread(this);
		GD.Print($"Connected {nbOfBlockers} controls for zoom block");
		// TODO change how this works, idk for now a better way
	}

	private static void GiveZoomBlock(Control node)
	{
		//GD.Print($"Gave :{nbOfBlockers++}");
		if (node == null) return;
		node.Connect("mouse_entered", new Callable(instance,"OnMouseEntered"));
		node.Connect("mouse_exited", new Callable(instance,"OnMouseExit"));
		//blockers.Add(node,false);
	}

	public static void Spread(Node parentNode)
	{
		if (parentNode is Control isControl)
		{
			GiveZoomBlock(isControl);
		}

		foreach (Node child in parentNode.GetChildren())
		{
			Spread(child);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!DisplayServer.WindowIsFocused())
		{
			blockingRn = 0;
		}
	}

	public void OnMouseEntered()
	{
		blockingRn++;
	}

	public void OnMouseExit()
	{
		if (blockingRn > 0)
		{
			blockingRn--;
		}
	}

	public static bool IsBlocking()
	{
		return blockingRn != 0;
	}
}
