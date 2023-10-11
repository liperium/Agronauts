using Godot;
using System;
using System.Collections.Generic;

public partial class NumberPerSecTooltip : Node2D
{
	private IdleNumber number;
	private LerpValue label;
	private long numberPerSec;
	private Queue<long> increaseQueue;

	private const float INTERVAL_TIME = 10f;
	public override void _Ready()
	{
		label = GetParent<IdleNumberLabel>().GetParent<LerpValue>();
		number = GetParent<IdleNumberLabel>().GetIdleNumber();
		increaseQueue = new Queue<long>();
		numberPerSec = 0;
		number.SetOnValueIncreased(AddAmount);
	}
	
	private void AddAmount(long amount)
	{
		long toAdd = (long)(amount / INTERVAL_TIME);
		numberPerSec += toAdd;
		increaseQueue.Enqueue(toAdd);
		SceneTreeTimer timer = GetTree().CreateTimer(INTERVAL_TIME);
		timer.Timeout += RemoveAmount;
		UpdateTooltip();
	}

	private void RemoveAmount()
	{
		numberPerSec -= increaseQueue.Dequeue();
		UpdateTooltip();
	}

	private void UpdateTooltip()
	{
		label.TooltipText = numberPerSec.FormattedNumber() + "/s";
	}

	public override void _ExitTree()
	{
		number.ResetOnValueIncreased(AddAmount);
	}
}



