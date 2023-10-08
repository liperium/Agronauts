using Godot;

public partial class firstTractorUpgradeUI : HBoxContainer
{
    private FirstTractorUpgrade firstTractorUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();
        
        firstTractorUpgrade = GameState.instance.upgrades.firstTractorUpgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        Visible = firstTractorUpgrade.IsUnlocked();
        firstTractorUpgrade.OnUnlock += OnUnlock;

        GetNode<UpgradeInfoContainer>("UpgradeInfoContainer").SetUpgrade(firstTractorUpgrade.GetInfo(),
            firstTractorUpgrade.GetCost(),
            ref firstTractorUpgrade.OnCostChanged);

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        if (!firstTractorUpgrade.CanBuy()) return;

        firstTractorUpgrade.Buy();
        
        firstTractorUpgrade.OnUnlock -= OnUnlock;
        buyButton.Pressed -= PressBuy;
        
        QueueFree();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
