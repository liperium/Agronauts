using Godot;
using System;

public partial class IdleUpgrade <TModifier> where TModifier : IdleModifier, new()
{
	protected TModifier modifier = new TModifier();
	IdleNumber affectedNumber;

	bool acquired;

	public virtual void Apply()
	{
		
	}
}
