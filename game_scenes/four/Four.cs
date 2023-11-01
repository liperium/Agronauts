using Godot;

public partial class Four : Control
{
	private Button manualButton;

	private Timer timer;
    private Timer chefTimer;
    private ProgressBar progressBar;
    [Export] public PackedScene numberIndicator;

	private bool automatic = false;

	private const float PROGRESS_START_TIME = 6.0f;
	private const float TIME_BEFORE_AUTO_COOK = 800f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		manualButton = GetNode<Button>("VBoxContainer/PanelContainer/Button");
		timer = GetNode<Timer>("Timer");
        chefTimer = GetNode<Timer>("ChefTimer");

        progressBar = GetNode<ProgressBar>("VBoxContainer/TextureRect/ProgressBar");

		GameState.instance.numbers.furnaceSpeed.SetOnValueChanged(UpdateProgressBarStats);
		UpdateProgressBarStats(GameState.instance.numbers.furnaceSpeed.GetValue());

		manualButton.Pressed += ButtonClicked;
		timer.Timeout += DoneBatch;
		chefTimer.Timeout += ButtonClickedAuto;


		if (GameState.instance.upgrades.autoFurnaceUpgrade.IsAcquired())
		{
			UnlockAutomatic();
		}
		else
		{
			GameState.instance.upgrades.autoFurnaceUpgrade.SetOnBuyUpgrade(UnlockAutomatic);
		}
		
	}

	public void UnlockAutomatic()
	{
		GameState.instance.upgrades.autoFurnaceUpgrade.ResetOnBuyUpgrade(UnlockAutomatic);
		automatic = true;
        chefTimer.WaitTime = TIME_BEFORE_AUTO_COOK / GameState.instance.numbers.furnaceAutoBakeSpeed.GetValue();
        chefTimer.Start();
    }



	public void UpdateProgressBarStats(long speed)
	{
		timer.WaitTime = PROGRESS_START_TIME / speed;
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
		
		if (timer.IsStopped())
		{
			if (CanBuy() == false)
			{
				GamePopUp.instance.AddToQueue(new GamePopUpInfo("KIMPOSSIBLE","KNEEDPOTATOS"));
				return;
			}
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
		GameState.instance.numbers.potatoCount.DecreaseValue(GameState.instance.numbers.furnaceBatchCount.GetValue());
	}

	private bool CanBuy()
	{
		return GameState.instance.numbers.potatoCount.GetValue() >=
		       GameState.instance.numbers.furnaceBatchCount.GetValue();
	}

	public void DoneBatch()
	{
		progressBar.Value = 0.0f;

		long gained = GameState.instance.numbers.cookedPotatoYield.GetValue() * 
		            GameState.instance.numbers.furnaceBatchCount.GetValue();
		
		GameState.instance.numbers.cookedPotatoCount.IncreaseValue(gained);
		
		manualButton.Disabled = false;
		if (automatic)
		{
            chefTimer.WaitTime = TIME_BEFORE_AUTO_COOK / GameState.instance.numbers.furnaceAutoBakeSpeed.GetValue();
            chefTimer.Start();
        }
		
		SpawnIndicator(gained);
	}

	public void SpawnIndicator(long value)
	{
		NumberIndicator indicatorInstance = numberIndicator.Instantiate<NumberIndicator>();
		indicatorInstance.SetFormat($"+{{0}}[img=30x30]{GameState.instance.numbers.cookedPotatoCount.GetImagePath()}[/img]");
		indicatorInstance.SetNumber(value);

		Node2D nodePos = GetNode<Node2D>("%IndicatorPos");
        
		nodePos.AddChild(indicatorInstance);
	}
}
