using Godot;
using System;

public partial class BuyOnHoldSetting : CheckBox
{

	public override void _Ready()
	{
		//save things
	}
	public override void _Toggled(bool buttonPressed)
	{
		FarmLand.buyOnHeld = buttonPressed;
	}
}
