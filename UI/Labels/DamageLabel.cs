using Godot;
using System;

public partial class DamageLabel : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(UpdateDamage);
		GameState.instance.numbers.potatoTemperature.SetOnValueChanged(UpdateDamage);
		GetParent<LerpValue>().Init(PotatoBullet.GetBulletDamage());
	}

	private void UpdateDamage(long value)
	{
		GetParent<LerpValue>().SetNewValue(PotatoBullet.GetBulletDamage());
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		GameState.instance.numbers.cookedPotatoCount.ResetOnValueChanged(UpdateDamage);
		GameState.instance.numbers.potatoTemperature.ResetOnValueChanged(UpdateDamage);
	}
}
