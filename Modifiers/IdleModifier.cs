using Godot;
using System;

public partial class IdleModifier
{
	protected IdleNumber owner;
	public virtual void Apply() { }
	public void SetOwner(IdleNumber owner)
	{
		this.owner = owner;
	}
}
