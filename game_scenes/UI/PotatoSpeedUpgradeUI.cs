using Godot;
public partial class PotatoSpeedUpgradeUI : HBoxContainer
{
    private PotatoSpeedUpgrade potatoSpeedUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();
        
        potatoSpeedUpgrade = GameState.instance.upgrades.potatoSpeedUpgrade;

        Visible = potatoSpeedUpgrade.IsUnlocked();
        potatoSpeedUpgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(potatoSpeedUpgrade.GetInfo(),
            potatoSpeedUpgrade.GetCost());
        potatoSpeedUpgrade.SetOnCostChanged((value) => infoContainer.UpdateCostText(value));

        buyButton = GetNode<TextureButton>("BuyButton");
        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        potatoSpeedUpgrade.Buy();
        
        potatoSpeedUpgrade.ResetOnUnlock(OnUnlock);
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
