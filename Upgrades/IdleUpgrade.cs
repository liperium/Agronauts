using Godot;
using System;

public partial class IdleUpgrade <TModifier> where TModifier : IdleModifier
{
	protected TModifier modifier;
	IdleNumber affectedNumber;

	bool acquired;

	public virtual void Apply()
	{
		
	}
}
