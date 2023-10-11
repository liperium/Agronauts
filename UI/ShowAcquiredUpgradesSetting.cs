using Godot;
using System;

public partial class ShowAcquiredUpgradesSetting : CheckBox
{
	public override void _Toggled(bool buttonPressed)
	{
		UIManager.ShowAcquiredUpgrades(buttonPressed);
	}
}
