using Godot;
using System;

public partial class PlayerCamera : Camera2D
{
	private const float MAX_ZOOM_IN = 5.0f;
	private const float MAX_ZOOM_OUT = 0.28f;


	private float zoomSens = 0.1f;
	private float lerpValue = 7.0f;
	private Vector2 targetZoom = Vector2.One;
	private bool cameraTransformMouse = false;

	private Vector2 homePosition = Vector2.Zero;

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event.IsAction("zoom_in") && targetZoom.X < MAX_ZOOM_IN && ZoomBlocker.CanZoom())
		{
			targetZoom += Vector2.One * zoomSens * targetZoom;
		}else if (@event.IsAction("zoom_out") && targetZoom.X > MAX_ZOOM_OUT && ZoomBlocker.CanZoom())
		{
			targetZoom -= Vector2.One * zoomSens * targetZoom;
		}


		if (cameraTransformMouse && @event is InputEventMouseMotion eventMouseMotion)
		{
			Position -= eventMouseMotion.Relative * (1.0f / targetZoom.X);
		}

		if (@event.IsAction("camera_snap_home"))
		{
			Position = homePosition;
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		homePosition = Position;

		float opacity =  0.0f;
		(GetNode<ColorRect>("../Clouds").Material as ShaderMaterial).Set("shader_parameter/transparency",opacity);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Zoom = Vector2.One * Mathf.Lerp(Zoom.X, targetZoom.X, lerpValue * (float)delta);
		if (Zoom.X < 1.0f)
		{
			float opacity =  1 - Zoom.X;
			(GetNode<ColorRect>("../Clouds").Material as ShaderMaterial).Set("shader_parameter/transparency",opacity);
			// SFX Bus
			AudioServer.SetBusVolumeDb((int)SoundCategory.GameSfx, (1-Zoom.X)*-25.0f);
			// Master Bus
			AudioServer.SetBusVolumeDb((int)SoundCategory.GameMaster, (1-Zoom.X)*-5.0f);
		}
		if (Input.IsActionPressed("mouse_control_camera"))
		{
			cameraTransformMouse = true;
		}else if(Input.IsActionJustReleased("mouse_control_camera"))
		{
			cameraTransformMouse = false;
		}
	}


}
