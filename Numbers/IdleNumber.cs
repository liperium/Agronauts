using Godot;
using System;
using System.Collections.Generic;
[Serializable]
public partial class IdleNumber : ISaveable
{
	public long value;
	private long added;
	private float multiplier;
	public string imagePath;

	public List<IdleModifier> multipliers;
	public List<IdleModifier> additions;

	private Action<long> OnValueChanged;
	private Action<long> OnValueIncreased;


	public void AddMultiplier(IdleModifier modifier)
	{
		if (multipliers == null)
		{
			multipliers = new List<IdleModifier>();
		}
		
		multipliers.Add(modifier);
	}

	public void RemoveMultiplier(IdleModifier modifier)
	{
		if (multipliers == null)
		{
			multipliers = new List<IdleModifier>();
		}
		multipliers.Remove(modifier);
	}
	
	public void AddAddition(IdleModifier modifier)
	{
		if (additions == null)
		{
			additions = new List<IdleModifier>();
		}
		
		additions.Add(modifier);
	}

	public void RemoveAddition(IdleModifier modifier)
	{
		if (additions == null)
		{
			additions = new List<IdleModifier>();
		}
		additions.Remove(modifier);
	}

	public bool HasModifier(IdleModifier modifier)
	{
		if (multipliers == null)
		{
			multipliers = new List<IdleModifier>();
		}

		if (additions == null)
		{
			additions = new List<IdleModifier>();
		}
		return multipliers.Contains(modifier) || additions.Contains(modifier);
	}

	public void UpdateValue(bool callValueChanged = true)
	{
		multiplier = 1.0f;
		if (multipliers != null)
		{
			foreach(IdleModifier im in multipliers)
			{
				im.Apply();
			}
		}

		added = 0;
		if (additions != null)
		{
			foreach (IdleModifier im in additions)
			{
				im.Apply();
			}
		}

		if (OnValueChanged != null && callValueChanged) OnValueChanged(GetValue());
	}

	public long GetValue()
	{
		return (long)((value+added) * multiplier);
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
	    if (multipliers != null)
	    {
		    foreach (IdleModifier modifier in multipliers)
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
