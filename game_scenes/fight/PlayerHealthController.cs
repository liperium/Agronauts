using Godot;
using System;

public partial class PlayerHealthController : Control
{
	[Export] private HealthBar healthBar;
	
	public override void _Ready()
	{
		healthBar.text.Text = "[img=50xz50]res://UI/sprites/PotatoeFumante.png[/img]";
		StyleBoxFlat fill = new StyleBoxFlat();
		fill.BgColor = new Color(0, 0.5f, 0);
		healthBar.AddThemeStyleboxOverride("fill",fill);
		healthBar.MaxValue = GameState.instance.numbers.cookedPotatoCount.GetValue();
		healthBar.Value = GameState.instance.numbers.cookedPotatoCount.GetValue();
		GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(OnCookedPotatoChanged);
	}
	
	private void OnCookedPotatoChanged(long potatos)
	{
		healthBar.Value = potatos;
	}
	
	public override void _ExitTree()
	{
		base._ExitTree();
		GameState.instance.numbers.cookedPotatoCount.ResetOnValueChanged(OnCookedPotatoChanged);
	}
}
