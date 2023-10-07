using Godot;
using System;
[Serializable]
public partial class IdleUpgrade <TModifier> where TModifier : IdleModifier, new()
{
	protected TModifier modifier;
	protected IdleNumber affectedNumber;
	protected InfoUpgrade info;

	public bool acquired;

	public virtual void Apply()
	{
		affectedNumber.AddModifier(modifier);
	}

	protected void SetAffectedNumber()
	{
		if(affectedNumber == null)
		{
			affectedNumber = GetAffectedNumber();
			modifier.SetOwner(affectedNumber);
		}
	}

	public virtual IdleNumber GetAffectedNumber()
	{
		return GameState.instance.numbers.potatoCount;
	}

	public virtual void InnitInfo()
	{
		info = new InfoUpgrade();
		info.SetName("ERROR");
		info.SetDescription("ERROR NOT SET");
		info.SetImagePath("res://theming/icon.svg");
    }

	public virtual void OnLoad()
	{
		modifier = new TModifier();
		InnitInfo();
	}
}
