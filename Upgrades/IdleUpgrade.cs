using Godot;
using System;

public partial class IdleUpgrade <TModifier> where TModifier : IdleModifier, new()
{
	protected TModifier modifier = new TModifier();
	protected IdleNumber affectedNumber;

	bool acquired;

	public IdleUpgrade()
	{
		SetAffectedNumber();
	}

	public virtual void Apply()
	{
		affectedNumber.AddModifier(modifier);
	}

	public virtual void SetAffectedNumber()
	{

	}
}
