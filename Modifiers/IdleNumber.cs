using Godot;
using System;
using System.Collections.Generic;
[Serializable]
public partial class IdleNumber
{
	public long value;
	public float multiplier = 1.0f;

	public List<IdleModifier> modifiers = new List<IdleModifier>();

	private Action<long> OnValueChanged;

	public void AddModifier(IdleModifier modifier)
	{
		modifiers.Add(modifier);
	}

	public void RemoveModifier(IdleModifier modifier)
	{
		modifiers.Remove(modifier);
	}

	public void UpdateValue()
	{
		multiplier = 1.0f;
		foreach(IdleModifier im in modifiers)
		{
			im.Apply();
		}
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
