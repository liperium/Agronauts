using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ZoomBlocker : Area2D
{

    private static Dictionary<int,bool> blockers = new Dictionary<int, bool>();

    private int id = 0;
    private static int idCount = 0;
    public override void _Ready()
    {
        id = idCount++;
        Connect("mouse_entered", new Callable(this,"OnMouseEntered"));
        Connect("mouse_exited", new Callable(this,"OnMouseExit"));
        blockers.Add(id,false);
    }

    public override void _ExitTree()
    {
        blockers.Remove(id);
    }


    private void OnMouseEntered()
    {
        blockers[id] = true;
    }
    private void OnMouseExit()
    {
        blockers[id] = false;
    }

    public static bool CanZoom()
    {
        if (blockers.Values.Contains(true))
        {
            return false;
        }
        return true;
    }
}