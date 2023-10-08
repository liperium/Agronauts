using Godot;

public partial class firstTractorUpgradeUI : HBoxContainer
{
    private FirstTractorUpgrade firstTractorUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();
        
        firstTractorUpgrade = GameState.instance.upgrades.firstTractorUpgrade;

        if (firstTractorUpgrade.acquired)
        {
            QueueFree();
            return;
        }
        
        Visible = firstTractorUpgrade.IsUnlocked();
        firstTractorUpgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(firstTractorUpgrade.GetInfo(),
            firstTractorUpgrade.GetCost());

        buyButton = GetNode<TextureButton>("BuyButton");
        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        if (!firstTractorUpgrade.CanBuy()) return;

        firstTractorUpgrade.Buy();
        
        firstTractorUpgrade.ResetOnUnlock(OnUnlock);
        buyButton.Pressed -= PressBuy;
        
        QueueFree();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
