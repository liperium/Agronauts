using Godot;
using System;

public partial class FlyingText : Control
{
	private Vector2 direction;
	private float speed;
	private Color color;
	private int sizePx;

	private double lifeSpan;
	private string text;
	[Export] private RichTextLabel label;

	private bool initiated;
	public Color Color
	{
		get => color;
		set => color = value;
	}
	public int SizePx
	{
		get => sizePx;
		set => sizePx = value;
	}
	public double LifeSpan
	{
		get => lifeSpan;
		set => lifeSpan = value;
	}

	public string Text
	{
		get => text;
		set => text = value;
	}

	public RichTextLabel Label
	{
		get => label;
	}


	
	public void Init()
	{
		if (color != null)
		{
			label.AddThemeColorOverride("default_color", color);
		}
		label.Text = text;

		if (sizePx != 0)
		{
			label.Set("theme_override_font_sizes/normal_font_size", sizePx);
		}

		Random rd = new Random();
		speed = rd.Next() % 10 + 3;

		float xDir = (float)(rd.Next() % 201 - 100) / 100;
		float yDir = (float)(rd.Next() % 201 - 100) / 100;
		direction = new Vector2(xDir, yDir);

		if (lifeSpan == 0) lifeSpan = 1f;

		initiated = true;

	}
	public override void _Process(double delta)
	{
		if (initiated == false) return;
		if (lifeSpan <= 0)
		{
			QueueFree();
		}

		this.Position = this.Position.MoveToward(this.Position + direction, (float)(speed * delta));
		lifeSpan -= delta;
	}
}
