using Godot;
using System;
using System.Collections.Generic;

public partial class NumberPerSecTooltip : Timer
{
	private IdleNumber number;
	private LerpValue label;
	private long numberPerSec;
	private long nextNumberPerSec;

	private const float INTERVAL_TIME = 5f;
	public override void _Ready()
	{
		label = GetParent<IdleNumberLabel>().GetParent<LerpValue>();
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
		label.TooltipText = numberPerSec.FormattedNumber() + "/s";
		Start();
	}

	public override void _ExitTree()
	{
		number.ResetOnValueIncreased(AddAmount);
	}
}



