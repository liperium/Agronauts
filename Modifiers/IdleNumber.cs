using Godot;
using System;
using System.Collections.Generic;

public partial class IdleNumber
{
	public long value;
	public float multiplier = 1.0f;

	public List<IdleModifier> modifiers;

	private Action<long> OnValueChanged;

	public void AddModifier(IdleModifier modifier)
	{

	}

	public void RemoveModifier(IdleModifier modifier)
	{

	}

	public void UpdateValue()
	{
		//TODO code d'update ici
		
		if (OnValueChanged != null) OnValueChanged(value);
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

	public void DecreaseValue(long value)
	{
		SetValue(this.value - value);
		if (value < 0) GD.Print("ON EST DANS LE NEGATIF DEFCON 5");
	}

	public void SetOnValueChanged(Action<long> action)
	{
		if (action == null)
		{
			GD.Print("ERROR ACTION IS NULL WTF");
		}
		else
		{
			OnValueChanged += action;
		}
	}
}
