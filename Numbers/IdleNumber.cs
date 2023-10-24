using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public partial class IdleNumber : ISaveable
{
	[JsonProperty]
	private long value;
	[JsonProperty]
	private long added;
	[JsonProperty]
	private float multiplier;
	
	//valeur finale affichée
	private long calculatedValue;

	private string imagePath = InfoUpgrade.defaultImagePath;

	private List<IdleModifier> modifiers;

	private IdleAction<long> OnValueChanged;
	private IdleAction<long> OnValueIncreased;

	public IdleNumber()
	{
		OnValueChanged = new IdleAction<long>();
		OnValueIncreased = new IdleAction<long>();
	}
	
	public virtual void OnLoad()
	{
		modifiers = new List<IdleModifier>();
		UpdateValue();
	}
	
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
		if (modifiers == null)
		{
			modifiers = new List<IdleModifier>();
		}
		modifiers.Remove(modifier);
	}
	
	public bool HasModifier(IdleModifier modifier)
	{
		if (modifiers == null)
		{
			modifiers = new List<IdleModifier>();
		}
		
		return modifiers.Contains(modifier);
	}

	public void UpdateValue(bool callValueChanged = true)
	{
		multiplier = 1.0f;
		added = 0;
		if (modifiers != null)
		{
			foreach(IdleModifier im in modifiers)
			{
				im.Apply();
			}
		}
		
		CalculateValue();
		
		if (callValueChanged) OnValueChanged.Invoke(GetValue());
	}

	private void CalculateValue()
	{
		calculatedValue = (long)((value + added) * multiplier);
	}

	public long GetValue()
	{
		return calculatedValue;
	}

	public void SetValue(long value, bool callOnValueChanged = true)
	{
		this.value = value;
		if (value < long.MaxValue)
		{
			//GD.Print("THIS IS NOT SUPPOSED TO BE CALL FOR OTHER REASON THAN TESTING");
			UpdateValue(callOnValueChanged);
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

	public long GetAdded()
	{
		return added;
	}

	public void SetAdded(long value)
	{
		added = value;
	}

	public void DecreaseValue(long value)
	{
		if (this.value - value <= 0)
		{
			SetValue(0);
		}
		else
		{
			SetValue(this.value - value);
		}
		if (value < 0) GD.PrintErr("ON EST DANS LE NEGATIF DEFCON 5");
	}

    public void IncreaseValue(long value)
    {
        SetValue(this.value + value);
		OnValueIncreased.Invoke(value);
        if (value < 0) GD.PrintErr("ON EST DANS LE NEGATIF DEFCON 5");
    }

    public void SetOnValueChanged(Action<long> action)
	{
		if (action == null)
		{
			GD.PrintErr("ERROR ACTION IS NULL WTF");
		}
		else
		{
			OnValueChanged += action;
		}
	}

    public void ResetOnValueChanged(Action<long> action)
    {
	    //OnValueChanged -= action;
    }

    public void SetOnValueIncreased(Action<long> action)
    {
        if (action == null)
        {
            GD.PrintErr("ERROR ACTION(INCREASE) IS NULL WTF");
        }
        else
        {
            OnValueIncreased += action;
        }
    }

    public void ResetOnValueIncreased(Action<long> action)
    {
        //OnValueIncreased -= action;
    }

    public string GetImagePath()
	{
		return imagePath;
	}

	public void SetImagePath(string path)
	{
		this.imagePath = path;
	}
}
