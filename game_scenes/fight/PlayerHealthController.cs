using Godot;
using System;

public partial class PlayerHealthController : Control
{
	[Export] private HealthBar healthBar;
	
	public override void _Ready()
	{
		healthBar.text.Text = $"{GameState.instance.numbers.cookedPotatoCount.GetValue().FormattedNumber()}[img=50xz50]res://UI/sprites/PotatoeFumante.png[/img]";
		StyleBoxFlat fill = new StyleBoxFlat();
		fill.BgColor = new Color(0, 0.5f, 0);
		fill.SetCornerRadiusAll(10);
		healthBar.AddThemeStyleboxOverride("fill",fill);
		healthBar.MaxValue = GameState.instance.numbers.cookedPotatoCount.GetValue();
		healthBar.Value = GameState.instance.numbers.cookedPotatoCount.GetValue();
		GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(OnCookedPotatoChanged);
	}
	
	private void OnCookedPotatoChanged(long potatos)
	{
		healthBar.Value = potatos;
		healthBar.text.Text = $"{potatos.FormattedNumber()}[img=50xz50]res://UI/sprites/PotatoeFumante.png[/img]";
	}
	
	public override void _ExitTree()
	{
		base._ExitTree();
		GameState.instance.numbers.cookedPotatoCount.ResetOnValueChanged(OnCookedPotatoChanged);
	}
}
