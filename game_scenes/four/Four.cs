using Godot;
using System;

public partial class Four : Control
{
	private Button manualButton;

	private Timer timer;

	private ProgressBar progressBar;

	private const float PROGRESS_START_TIME = 6.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		manualButton = GetNode<Button>("VBoxContainer/Button");
		timer = GetNode<Timer>("Timer");
		progressBar = GetNode<ProgressBar>("VBoxContainer/ProgressBar");


		UpdateProgressBarStats(PROGRESS_START_TIME);

		manualButton.Pressed += ButtonClicked;
		timer.Timeout += DoneBatch;
	}



	public void UpdateProgressBarStats(float newBatchTime)
	{
		timer.WaitTime = newBatchTime;
		progressBar.MaxValue = timer.WaitTime;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!timer.IsStopped())
		{
			progressBar.Value = timer.WaitTime-timer.TimeLeft;
		}
	}

	public void ButtonClicked()
	{
		if (timer.IsStopped() && CanBuy())
		{
			manualButton.Disabled = true;
			Buy();
			timer.Start();
		}
	}

	private void Buy()
	{
		GameState.instance.numbers.potatoCount.DecreaseValue(GameState.instance.numbers.cookedPotatoYield.GetValue());
	}

	private bool CanBuy()
	{
		return GameState.instance.numbers.potatoCount.GetValue() >=
		       GameState.instance.numbers.furnaceBatchCount.GetValue();
	}

	public void DoneBatch()
	{
		progressBar.Value = 0.0f;
		
		GameState.instance.numbers.cookedPotatoCount.IncreaseValue(
			GameState.instance.numbers.cookedPotatoYield.GetValue() * 
			GameState.instance.numbers.furnaceBatchCount.GetValue());
		
		manualButton.Disabled = false;
	}
}
