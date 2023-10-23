using Godot;
using System;

public partial class HideManualTractor : CheckBox
{
	public static IdleAction<bool> OnHideCheck;

	private static bool hidden;

	public HideManualTractor()
	{
		OnHideCheck = new();
	}

	public static void SetOnHideCheck(Action<bool> action)
	{
		if (action == null)
		{
			GD.PrintErr("ERROR ACTION IS NULL WTF");
		}
		else
		{
			OnHideCheck += action;
		}
	}

	public static void ResetOnHideCheck(Action<bool> action)
	{
		OnHideCheck.RemoveManual(action);
	}

	public static bool IsHidden()
	{
		return hidden;
	}

	public override void _Toggled(bool buttonPressed)
	{
		hidden = buttonPressed;
		OnHideCheck.Invoke(buttonPressed);
	}
}
