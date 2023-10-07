using Godot;
using System;
using System.Collections.Generic;

public partial class IdleNumber
{
	long value;
	float multiplier;

	List<IdleModifier> modifiers;

	public Action<long> OnValueChanged;

	public void AddModifier(IdleModifier modifier)
	{

	}

	public void RemoveModifier(IdleModifier modifier)
	{

	}

	public void UpdateValue()
	{
		//TODO code d'update ici
		
		OnValueChanged(value);
	}

	public long GetValue()
	{
		return (long)(value * multiplier);
	}

	public void SetValue(long value)
	{
		this.value = value;
		GD.Print("THIS IS NOT SUPPOSED TO BE CALL FOR OTHER REASON THAN TESTING");
		UpdateValue();
	}
}
