using System;

[Serializable]
public partial class IdleUpgrade <TModifier> : BaseIdleUpgrade where TModifier : IdleModifier, new()
{
	public int modifierId;
	protected TModifier modifier;
	protected IdleNumber affectedNumber;
	
	public virtual void Apply()
	{
		if (!affectedNumber.HasModifier(modifier))
		{
			affectedNumber.AddModifier(modifier);
		}
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

	public override void OnLoad()
	{
		base.OnLoad();
		
		if (modifierId == 0)
		{
			this.modifier = new TModifier();
			modifierId = GameState.allModifiers.Count + 1;
			this.modifier.id = modifierId;
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
                this.modifier.id = modifierId;
				while (GameState.allModifiers.ContainsKey(modifierId)) modifierId++;
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
