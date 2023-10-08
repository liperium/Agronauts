using Godot;
using System;

public partial class Four : Control
{
	private Button manualButton;

	private Timer timer;
    private Timer chefTimer;
    private ProgressBar progressBar;

	private bool automatic = false;

	private const float PROGRESS_START_TIME = 6.0f;
	private const float TIME_BEFORE_AUTO_COOK = 2000f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		manualButton = GetNode<Button>("VBoxContainer/Button");
		timer = GetNode<Timer>("Timer");
        chefTimer = GetNode<Timer>("ChefTimer");

        progressBar = GetNode<ProgressBar>("VBoxContainer/ProgressBar");


		UpdateProgressBarStats(PROGRESS_START_TIME);

		manualButton.Pressed += ButtonClicked;
		timer.Timeout += DoneBatch;
		chefTimer.Timeout += ButtonClickedAuto;
		GameState.instance.upgrades.autoFurnaceUpgrade.OnUnlock += UnlockAutomatic;
		
	}

	public void UnlockAutomatic()
	{
		GameState.instance.upgrades.autoFurnaceUpgrade.OnUnlock -= UnlockAutomatic;
		automatic = true;
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

    public void ButtonClickedAuto()
    {
        if (timer.IsStopped() && CanBuy())
        {
            manualButton.Disabled = true;
            Buy();
            timer.Start();
            GameState.instance.numbers.furnaceTotalAutoCookedPotato.IncreaseValue(
            GameState.instance.numbers.cookedPotatoYield.GetValue() *
            GameState.instance.numbers.furnaceBatchCount.GetValue());
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
		timer.WaitTime = TIME_BEFORE_AUTO_COOK / GameState.instance.numbers.furnaceAutoBakeSpeed.GetValue();
		chefTimer.Start();
	}
}
