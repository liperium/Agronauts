using Godot;
using System;
using System.Collections.Generic;

public partial class ZoomBlock : Control
{
	private static Dictionary<int, bool> blockers = new Dictionary<int, bool>();
	public static bool blocking = false;
	private static int idCount = 0;

	private int id;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		id = idCount++;
		Connect("mouse_entered", new Callable(this,"OnMouseEntered"));
		Connect("mouse_exited", new Callable(this,"OnMouseExit"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnMouseEntered()
	{
		blockers[id] = true;
	}

	public void OnMouseExit()
	{
		blockers[id] = false;
	}

	public static bool IsBlocking()
	{
		foreach (var block in blockers.Values)
		{
			if (block)
			{
				return true;
			}
		}
		return false;
	}
}
