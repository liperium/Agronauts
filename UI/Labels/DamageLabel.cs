using Godot;
using System;

public partial class DamageLabel : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetParent().GetParent().GetParent<BoxContainer>().Hide();
		
		if (GameState.instance.upgrades.unlockFurnaceUpgrade.IsUnlocked())
		{
			Init();
		}
		else
		{
			GameState.instance.upgrades.unlockFurnaceUpgrade.SetOnUnlock(Init);
		}
	}

	private void UpdateDamage(long value)
	{
		GetParent<LerpValue>().SetNewValue(PotatoBullet.GetBulletDamage());
	}

	private void Init()
	{
		GetParent().GetParent().GetParent<BoxContainer>().Show();
		GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(UpdateDamage);
		GameState.instance.numbers.potatoTemperature.SetOnValueChanged(UpdateDamage);
		
		if (GameState.instance.numbers.cookedPotatoCount.GetValue() == 0)
		{
			GetParent<LerpValue>().Init(0);
		}
		else
		{
			GetParent<LerpValue>().Init(PotatoBullet.GetBulletDamage());
		}
	}
}
