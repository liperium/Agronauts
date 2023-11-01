using Godot;
using System;

public partial class Flash : Node
{
	private ShaderMaterial flashShader;

	private bool flashOnStart = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		flashShader = GetParent<CanvasItem>().Material as ShaderMaterial;
		if (flashOnStart) Start();
		else Stop();
	}

	public void FlashOnReady()
	{
		flashOnStart = true;
	}
	public void Start()
	{
		flashShader.Set("shader_parameter/mode",1);
	}

	public void Stop()
	{
		flashShader.Set("shader_parameter/mode",0);
	}
}
