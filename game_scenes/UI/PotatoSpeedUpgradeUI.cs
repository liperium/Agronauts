using Godot;
public partial class PotatoSpeedUpgradeUI : HBoxContainer
{
    private PotatoSpeedUpgrade potatoSpeedUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();
        
        potatoSpeedUpgrade = GameState.instance.upgrades.potatoSpeedUpgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        Visible = potatoSpeedUpgrade.IsUnlocked();
        potatoSpeedUpgrade.OnUnlock += OnUnlock;

        GetNode<UpgradeInfoContainer>("UpgradeInfoContainer").SetUpgrade(potatoSpeedUpgrade.GetInfo(),
            potatoSpeedUpgrade.GetCost(),
            ref potatoSpeedUpgrade.OnCostChanged);

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        potatoSpeedUpgrade.Buy();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
