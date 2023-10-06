using Godot;
using System;
using System.Collections.Generic;

public partial class IdleNumber
{
	long value;
	float multiplier;

	List<IdleModifier> modifiers;

	public void AddModifier(IdleModifier modifier)
	{

	}

	public void RemoveModifier(IdleModifier modifier)
	{

	}

	public void UpdateValue()
	{

	}

	public long GetValue()
	{
		return (long)(value * multiplier);
	}
}
