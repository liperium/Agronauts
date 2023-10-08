using Godot;
using System;
[Serializable]
public partial class IdleUpgrade <TModifier> where TModifier : IdleModifier, new()
{
	public int modifierId;
	protected TModifier modifier;
	protected IdleNumber affectedNumber;
	protected InfoUpgrade info;

	public bool acquired;


	public virtual void Apply()
	{
		affectedNumber.AddModifier(modifier);
	}

	protected void SetAffectedNumber()
	{
		if(affectedNumber == null)
		{
			affectedNumber = GetAffectedNumber();
			modifier.SetOwner(affectedNumber);
		}
	}


	public virtual IdleNumber GetAffectedNumber()
	{
		return GameState.instance.numbers.potatoCount;
	}

	public InfoUpgrade GetInfo()
	{
		return info;
	}

	public virtual void InnitInfo()
	{
		info = new InfoUpgrade();
		info.SetName("ERROR");
		info.SetDescription("ERROR NOT SET");
		info.SetImagePath("res://theming/icon.svg");
    }

	public virtual void OnLoad()
	{
		if (modifierId == 0)
		{
			this.modifier = new TModifier();
			modifierId = GameState.allModifiers.Count + 1;
			GameState.allModifiers.Add(modifierId, modifier);
		}
		else
		{
			IdleModifier newModifier;
			if (GameState.allModifiers.TryGetValue(modifierId, out newModifier))
			{
				this.modifier = (TModifier)newModifier;
			}
			else
			{
				this.modifier = new TModifier();
				modifierId = GameState.allModifiers.Count + 1;
				GameState.allModifiers.Add(modifierId, this.modifier);
			}
		}
		InnitInfo();
	}

	public virtual string GetEffectText()
	{
		return "";
	}
}
