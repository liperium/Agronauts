using Godot;
using System;

public partial class HideManualTractor : CheckBox
{
	public static Action<bool> OnHideCheck;

	private static bool hidden;
	public static void SetOnHideCheck(Action<bool> action)
	{
		if (action == null)
		{
			GD.Print("ERROR ACTION IS NULL WTF");
		}
		else
		{
			OnHideCheck += action;
		}
	}

	public static void ResetOnHideCheck(Action<bool> action)
	{
		OnHideCheck -= action;
	}

	public static bool IsHidden()
	{
		return hidden;
	}

	public override void _Toggled(bool buttonPressed)
	{
		hidden = buttonPressed;
		if (OnHideCheck != null) OnHideCheck(buttonPressed);
	}
}
