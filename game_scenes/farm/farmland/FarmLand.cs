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

	public static CompressedTexture2D WildTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/terre_wild.png");

	public static CompressedTexture2D BaseTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/terre_owned.png");

	public static CompressedTexture2D LaboureTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/terre_laboure.png");

	public static CompressedTexture2D PlantedTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/terre_plante1.png");

	public static CompressedTexture2D ReadyTexture =
		ResourceLoader.Load<CompressedTexture2D>(
			"res://game_scenes/farm/farmland/tiles/terre_pret1.png");

	private TextureButton button;
	private Timer growthTimer;
	private ProgressBar progressBar;
	private LandState currState = LandState.Wild;
	private Label priceLabel;
	public long cost = 0;
	public LandState CurrState => currState;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		button = GetNode<TextureButton>("TextureButton");
		button.Pressed += Clicked;
		button.TextureNormal = WildTexture;

		growthTimer = GetNode<Timer>("Timer");
		growthTimer.OneShot = true;

		priceLabel = GetNode<Label>("PriceLabel");


		//TODO link with global timer

		growthTimer.Timeout += Grown;
		MouseEntered += Hovered;
		MouseExited += UnHovered;

		progressBar = GetNode<ProgressBar>("ProgressBar");
		UpdateProgressBarStats(3.0f);
	}

	public void ChangeCost(long newCost)
	{
		cost = newCost;
		priceLabel.Text = cost.FormattedNumber();
	}



	public void Clicked()
	{
		//GD.Print("State "+currState);
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
				Laboure();//TODO What to do with base?
				break;
			case LandState.Laboure:
				Plant();
				break;
			case LandState.Planted: break;
			case LandState.Ready:
				Harvest();
				break;
		}
	}

	private bool CanBuy()
	{
		return GameState.instance.numbers.potatoCount.GetValue() >= cost;
	}

	public void Hovered()
	{
		priceLabel.Visible = true;

	}
	private void UnHovered()
	{
		priceLabel.Visible = false;
	}
	public void Bought()
	{
		GameState.instance.numbers.potatoCount.DecreaseValue(cost);
		
		button.TextureNormal = BaseTexture;
		currState = LandState.Base;
		priceLabel.QueueFree();
		MouseEntered -= Hovered;
		MouseExited -= UnHovered;
		
		// Check if it can expand
		GetParent<FarmField>().Expand(position);
	}
	public void Laboure()
	{
		button.TextureNormal = LaboureTexture;
		currState = LandState.Laboure;
	}
	public void Plant()
	{
		button.TextureNormal = PlantedTexture;
		currState = LandState.Planted;
		growthTimer.Start();
		progressBar.Visible = true;
	}
	public void Harvest()
	{
		button.TextureNormal = LaboureTexture;
		currState = LandState.Laboure;
		progressBar.Visible = false;

		GameState.instance.numbers.potatoCount.IncreaseValue(GameState.instance.numbers.potatoYield.GetValue());
	}

	public void Grown()
	{
		progressBar.Visible = false;
		button.TextureNormal = ReadyTexture;
		currState = LandState.Ready;
	}

	public void UpdateProgressBarStats(float newProgressTime)
	{
		growthTimer.WaitTime = newProgressTime;
		progressBar.MaxValue = newProgressTime;
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
