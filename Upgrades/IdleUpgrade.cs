using System;

[Serializable]
public partial class IdleUpgrade <TModifier> : BaseIdleUpgrade where TModifier : IdleModifier, new()
{
	protected TModifier modifier;
	protected IdleNumber affectedNumber;
	
	public virtual void Apply()
	{
		modifier.AddModifier();
	}

	protected void SetAffectedNumber()
	{
		if(affectedNumber == null)
		{
			affectedNumber = GetAffectedNumber();
			modifier.SetOwner(affectedNumber);
		}
	}

	/// <summary>
	/// Gets the IdleNumber affected by this upgrade. This needs to be overridden for each upgrade.
	/// </summary>
	/// <returns>The IdleNumber affected by this upgrade.</returns>
	public virtual IdleNumber GetAffectedNumber()
	{
		return GameState.instance.numbers.potatoCount;
	}

	public override void OnLoad()
	{
		base.OnLoad();
		modifier = new TModifier();
		modifier.SetOwner(GetAffectedNumber());
		UpdateModifier();
		Apply();
		GetAffectedNumber().UpdateValue();
		InitInfo();
	}

	/// <summary>
	/// Updates the modifier. Called on load and on buy
	/// </summary>
	public virtual void UpdateModifier()
	{
		
	}
}
