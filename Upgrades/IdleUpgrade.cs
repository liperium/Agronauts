using Godot;
using System;

public partial class IdleUpgrade <TModifier> where TModifier : IdleModifier, new()
{
	protected TModifier modifier = new TModifier();
	protected IdleNumber affectedNumber;
	protected InfoUpgrade info = new();

	bool acquired;


	public IdleUpgrade()
	{
		InnitInfo();
	}


	public virtual void Apply()
	{
		if(affectedNumber == null)
		{
			affectedNumber = GameState.instance.numbers.potatoYield;
			modifier.SetOwner(affectedNumber);
		}
		affectedNumber.AddModifier(modifier);
	}

	public virtual void SetAffectedNumber()
	{

	}

	public virtual void InnitInfo()
	{
		info.SetName("ERROR");
		info.SetDescription("ERROR NOT SET");
		info.SetImagePath("res://theming/icon.svg");
    }
}
