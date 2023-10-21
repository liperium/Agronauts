using Godot;
using System;
using System.Collections.Generic;

public partial class NumberPerSecTooltip : Timer
{
	private IdleNumber number;
	[Export] private RichTextLabel tooltip;
	[Export] private string text;
	private long numberPerSec;
	private long nextNumberPerSec;

	private const float INTERVAL_TIME = 5f;
	public override void _Ready()
	{
		number = GetParent<IdleNumberLabel>().GetIdleNumber();
		numberPerSec = 0;
		nextNumberPerSec = 0;
		number.SetOnValueIncreased(AddAmount);
		WaitTime = INTERVAL_TIME;
		Timeout += UpdateTooltip;
		Start();
	}
	
	private void AddAmount(long amount)
	{
		nextNumberPerSec += (long)(amount / INTERVAL_TIME);
	}
	

	private void UpdateTooltip()
	{
		numberPerSec = nextNumberPerSec;
		nextNumberPerSec = 0;
		tooltip.TooltipText = $"{numberPerSec.FormattedNumber()}/s - {Tr(text)}";
		Start();
	}

	public override void _ExitTree()
	{
		number.ResetOnValueIncreased(AddAmount);
	}
}



