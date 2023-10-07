using Godot;
using System;

public partial class YieldNumber : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameState.instance.numbers.potatoYield.SetOnValueChanged(UpdateYield);
        if (GameState.SAVE_ENABLED == false) GameState.instance.numbers.potatoYield.SetValue(100);
	}

	public void UpdateYield(long number)
	{
		Text = "Yield : " + GameState.instance.numbers.potatoYield.GetValue();
	}

	
}
