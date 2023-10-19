using Godot;
using System;
[Serializable]
public partial class IdleModifier
{
	public int id;
	protected IdleNumber owner;
	public virtual void Apply() { }
	
	public virtual void AddModifier(){}
	public void SetOwner(IdleNumber owner)
	{
		this.owner = owner;
	}
}
