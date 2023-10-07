using Godot;
using System;

public partial class PlayerCamera : Camera2D
{

	private float zoomSens = 0.1f;
	private float lerpValue = 7.0f;
	private CanvasLayer uiLayer;
	private Vector2 targetZoom = Vector2.One;
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event.IsAction("zoom_in"))
		{
			targetZoom += Vector2.One* 1.0f * zoomSens;
		}else if (@event.IsAction("zoom_out"))
		{
			targetZoom -= Vector2.One* 1.0f * zoomSens;
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		uiLayer=GetNode<CanvasLayer>("UI");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Zoom = Vector2.One * Mathf.Lerp(Zoom.X, targetZoom.X, lerpValue * (float)delta);
		uiLayer.FollowViewportScale = 1/Zoom.X;
	}


}
