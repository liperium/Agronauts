using Godot;
using System;
using System.Collections.Generic;
[Serializable]
public partial class IdleNumber : ISaveable
{
	public long value;
	private float multiplier;
	public string imagePath;

	public List<IdleModifier> modifiers;

	private Action<long> OnValueChanged;
	private Action<long> OnValueIncreased;


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
		if (this.value - value <= 0)
		{
			SetValue(0);
		}
		else
		{
			SetValue(this.value - value);
		}
		if (value < 0) GD.Print("ON EST DANS LE NEGATIF DEFCON 5");
	}

    public void IncreaseValue(long value)
    {
        SetValue(this.value + value);
		if(OnValueIncreased != null) { OnValueIncreased(value); }
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
        OnValueIncreased -= action;
    }

    public void OnLoad()
    {
	    if (modifiers != null)
	    {
		    foreach (IdleModifier modifier in modifiers)
		    {
			    int newId = modifier.id;
			    GameState.allModifiers.Add(newId, modifier);
			    modifier.SetOwner(this);
		    }
	    }
	    UpdateValue(false);
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
