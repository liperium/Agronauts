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

	private TextureButton button;
	private Timer growthTimer;
	private ProgressBar progressBar;
	private LandState currState = LandState.Wild;
	private Label priceLabel;
	public long cost = 0;
	public LandState CurrState => currState;

	private static Random random = new Random();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		button = GetNode<TextureButton>("TextureButton");
		button.Pressed += Clicked;
		button.TextureNormal = GetRandomTexture(WildTextures);

		growthTimer = GetNode<Timer>("Timer");
		growthTimer.OneShot = true;

		priceLabel = GetNode<Label>("PriceLabel");


		//TODO link with global timer

		growthTimer.Timeout += Grown;
		MouseEntered += Hovered;
		MouseExited += UnHovered;

		progressBar = GetNode<ProgressBar>("ProgressBar");
		
		FarmFieldMaster.OnFarmTimeChange += UpdateProgressBarStats;
		UpdateProgressBarStats();

		Hide();
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
		priceLabel.Text = cost.FormattedNumber();
	}

	public void Show()
	{
		Visible = true;
		GetNode<AnimationTree>("AnimationTree").Active = true;
		button.MouseFilter = Control.MouseFilterEnum.Stop;
	}

	public void Hide()
	{
		Visible = false;
		button.MouseFilter = Control.MouseFilterEnum.Ignore;
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
		
		button.TextureNormal = GetRandomTexture(BoughtTextures);
		currState = LandState.Base;
		priceLabel.QueueFree();
		MouseEntered -= Hovered;
		MouseExited -= UnHovered;
		
		// Check if it can expand
		GetParent<FarmField>().LandBought(new Pos2D(position.X,position.Y));
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
		progressBar.Visible = true;
	}
	public void Harvest()
	{
		button.TextureNormal = GetRandomTexture(LaboureTextures);
		currState = LandState.Laboure;
		progressBar.Visible = false;

		GameState.instance.numbers.potatoCount.IncreaseValue(GameState.instance.numbers.potatoYield.GetValue());
	}

	public void Grown()
	{
		progressBar.Visible = false;
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
