using Godot;
using System;
using System.Collections.Generic;
[Serializable]
public partial class IdleNumber
{
	public long value;
	private float multiplier;

	public List<IdleModifier> modifiers;

	private Action<long> OnValueChanged;


	public void AddModifier(IdleModifier modifier)
	{
		if (modifiers == null)
		{
			modifiers = new List<IdleModifier>();
		}
		
		modifiers.Add(modifier);
	}

	public void RemoveModifier(IdleModifier modifier)
	{
		modifiers.Remove(modifier);
	}

	public void UpdateValue(bool callValueChanged = true)
	{
		multiplier = 1.0f;
		if (modifiers != null)
		{
			foreach(IdleModifier im in modifiers)
			{
				im.Apply();
			}
		}

		if (OnValueChanged != null && callValueChanged) OnValueChanged(GetValue());
	}

	public long GetValue()
	{
		return (long)(value * multiplier);
	}

	public void SetValue(long value)
	{
		this.value = value;
		if (value < long.MaxValue)
		{
			//GD.Print("THIS IS NOT SUPPOSED TO BE CALL FOR OTHER REASON THAN TESTING");
			UpdateValue();
		}
	}

	public float GetMultiplier()
	{
		return multiplier;
	}

	public void SetMultiplier(float value)
	{
		multiplier = value;
	}

	public void DecreaseValue(long value)
	{
		SetValue(this.value - value);
		if (value < 0) GD.Print("ON EST DANS LE NEGATIF DEFCON 5");
	}

    public void IncreaseValue(long value)
    {
        SetValue(this.value + value);
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

    public void ResetOnValueChanged(Action<long> action)
    {
	    OnValueChanged -= action;
    }

    public void OnLoad()
    {
	    if (modifiers != null)
	    {
		    foreach (IdleModifier modifier in modifiers)
		    {
			    int newId = GameState.allModifiers.Count + 1;
			    GameState.allModifiers.Add(newId, modifier);
			    modifier.SetOwner(this);
		    }
	    }
	    UpdateValue(false);
    }
}
