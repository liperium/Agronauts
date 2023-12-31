using Godot;
using System;
using System.Threading;
using Timer = Godot.Timer;

public partial class FarmLand : Area2D
{
	public enum LandState
	{
		Wild,
		Base,
		Laboure,
		Planted,
		Ready
	}

	public Pos2D position = new Pos2D(0,0);

	private static string baseFolder = "res://game_scenes/farm/farmland/tiles/";
	public static CompressedTexture2D[] WildTextures =
	{
		ResourceLoader.Load<CompressedTexture2D>(baseFolder+"terre_wild.png"),
	};
	public static CompressedTexture2D[] BoughtTextures = {
		ResourceLoader.Load<CompressedTexture2D>(baseFolder+"terre_achete.png"),
	};
	public static CompressedTexture2D[] LaboureTextures = {
		ResourceLoader.Load<CompressedTexture2D>(baseFolder+"terre_laboure.png"),
	};
	public static CompressedTexture2D[] PlantedTextures = {
		ResourceLoader.Load<CompressedTexture2D>(baseFolder+"terre_plante1.png"),
		ResourceLoader.Load<CompressedTexture2D>(baseFolder+"terre_plante2.png"),
		ResourceLoader.Load<CompressedTexture2D>(baseFolder+"terre_plante3.png"),
	};
	public static CompressedTexture2D[] ReadyTextures = {
		ResourceLoader.Load<CompressedTexture2D>(baseFolder+"terre_pret1.png"),
		ResourceLoader.Load<CompressedTexture2D>(baseFolder+"terre_pret2.png"),
		ResourceLoader.Load<CompressedTexture2D>(baseFolder+"terre_pret3.png"),
	};
	public static AudioStreamWav harvestSound =
		ResourceLoader.Load<AudioStreamWav>("res://game_scenes/farm/farmland/sfx/potato_harvested.wav");
	public static AudioStreamWav buySound =
		ResourceLoader.Load<AudioStreamWav>("res://game_scenes/farm/farmland/sfx/buy_land.wav");
	private TextureButton button;
	private Timer growthTimer;
	private ProgressBar progressBar;
	private LandState currState = LandState.Wild;
	private RichTextLabel priceLabel;
	[Export] public AudioStreamPlayer2D audioPlayer;
	[Export] public PackedScene pickupIndicator;
	public long cost = 0;
	public LandState CurrState => currState;

	private static Random random = new Random();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		button = GetNode<TextureButton>("TextureButton");
		button.Pressed += Clicked;
		button.TextureNormal = GetRandomTexture(BoughtTextures);

		growthTimer = GetNode<Timer>("Timer");
		growthTimer.OneShot = true;

		priceLabel = GetNode<RichTextLabel>("CenterContainer/PriceLabel");
		priceLabel.Show();
		
		growthTimer.Timeout += Grown;

		progressBar = GetNode<ProgressBar>("ProgressBar");
		
		FarmFieldMaster.OnFarmTimeChange += UpdateProgressBarStats;
		UpdateProgressBarStats();

		HideRemoveInteract();
	}

	public CompressedTexture2D GetRandomTexture(CompressedTexture2D[] choices)
	{
		if (choices.Length == 1)
		{
			return choices[0];
		}
		else
		{
			return choices[random.Next(0, choices.Length)];
		}

	}

	public void ChangeCost(long newCost)
	{
		cost = newCost;
		priceLabel.Text = $"[center]{cost.FormattedNumber()}\n[img=30x30]res://Icons/Potato.png[/img][/center]";
	}

	public void FirstShow()
	{
		Show();
		GetNode<AnimationTree>("AnimationTree").Active = true;
		button.MouseFilter = Control.MouseFilterEnum.Stop;
	}

	public void HideRemoveInteract()
	{
		Hide();
		button.MouseFilter = Control.MouseFilterEnum.Ignore;
	}


	public void Clicked()
	{
		switch (currState)
		{
			case LandState.Wild:
				if (CanBuy())
				{
					Bought();
					Laboure();
				}
				break;
			case LandState.Base:
				Laboure();
				break;
			case LandState.Laboure:
				Plant();
				break;
			case LandState.Planted: break;
			case LandState.Ready:
				Harvest(true);
				break;
		}
	}

    public void HeldClicked()
    {
		if (currState == LandState.Wild && !GameState.settings.buyLandOnHold) return;
		Clicked();
    }

    private bool CanBuy()
	{
		return GameState.instance.numbers.potatoCount.GetValue() >= cost;
	}


	public override void _MouseEnter()
	{
		if (Input.IsActionPressed("upgrade_tile"))
		{
			HeldClicked();
		}
    }
	public void Bought(bool pay = true, bool addToHistory = true)
	{
		if (pay)
		{
			GameState.instance.numbers.potatoCount.DecreaseValue(cost);
			GameState.instance.numbers.numberOfTilesUnlocked.IncreaseValue(1);
			
			audioPlayer.Stream = buySound;
			audioPlayer.Play();
		}

		button.TextureNormal = GetRandomTexture(BoughtTextures);
		currState = LandState.Base;
		priceLabel.QueueFree();

		// Check if it can expand
		GetParent<FarmField>().LandBought(new Pos2D(position.X,position.Y), addToHistory);
	}
	public void Laboure()
	{
		button.TextureNormal = GetRandomTexture(LaboureTextures);
		currState = LandState.Laboure;
	}
	public void Plant()
	{
		button.TextureNormal = GetRandomTexture(PlantedTextures);
		currState = LandState.Planted;
		growthTimer.Start();
		progressBar.Show();
	}
	public void Harvest(bool manual = false)
	{
		button.TextureNormal = GetRandomTexture(LaboureTextures);
		currState = LandState.Laboure;
		progressBar.Hide();

		audioPlayer.Stream = harvestSound;
		audioPlayer.Play();

		long potatoYield = GameState.instance.numbers.potatoYield.GetValue();
		GameState.instance.numbers.potatoCount.IncreaseValue(potatoYield);

		if (manual)
		{
			SpawnIndicator(potatoYield);
		}
	}

	private void SpawnIndicator(long value)
	{
		NumberIndicator indicatorInstance = pickupIndicator.Instantiate<NumberIndicator>();
		indicatorInstance.SetFormat($"+{{0}}[img=30x30]{GameState.instance.numbers.potatoCount.GetImagePath()}[/img]");
		indicatorInstance.SetNumber(value);
		indicatorInstance.GlobalPosition = GlobalPosition;
		indicatorInstance.SetScale(0.5f);
		
		GetTree().CurrentScene.AddChild(indicatorInstance);
	}

	public void Grown()
	{
		progressBar.Hide();
		button.TextureNormal = GetRandomTexture(ReadyTextures);
		currState = LandState.Ready;
	}

	public void UpdateProgressBarStats()
	{
		growthTimer.WaitTime = FarmFieldMaster.farmTime;
		progressBar.MaxValue = FarmFieldMaster.farmTime;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (!growthTimer.IsStopped())
		{
			progressBar.Value = growthTimer.WaitTime - growthTimer.TimeLeft;
		}
	}
}
