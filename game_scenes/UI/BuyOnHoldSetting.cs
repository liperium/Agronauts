using Godot;
using System;

public partial class BuyOnHoldSetting : CheckBox
{

	public override void _Ready()
	{
		ButtonPressed = GameState.settings.buyLandOnHold;
	}
	public override void _Toggled(bool buttonPressed)
	{
		FarmLand.buyOnHeld = buttonPressed;
		GameState.settings.buyLandOnHold = buttonPressed;
		GameState.SaveSettings();
	}
}
