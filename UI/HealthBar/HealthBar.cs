using Godot;
using System;

public partial class HealthBar : ProgressBar
{

	private long maxHealth;

	private long currentHealth;

	public long MaxHealth => maxHealth;

	public long CurrentHealth => currentHealth;

	[Export] public RichTextLabel text;

	public void SetHealth(long health)
	{
		currentHealth = health;
		this.Value = health;
	}

	public void SetMaxHealth(long maxHealth)
	{
		this.maxHealth = maxHealth;
		this.MaxValue = maxHealth;
	}
	
	
	public override void _Ready()
	{
		text.Set("theme_override_font_sizes/normal_font_size", Size.Y * 0.8f);
	}
	
}
