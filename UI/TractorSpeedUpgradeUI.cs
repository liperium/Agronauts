using Godot;
using System;

public partial class TractorSpeedUpgradeUI : HBoxContainer
{
    private TractorSpeedUpgrade tractorSpeedUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        tractorSpeedUpgrade = GameState.instance.upgrades.tractorSpeedUpgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        Visible = tractorSpeedUpgrade.IsUnlocked();
        tractorSpeedUpgrade.OnUnlock += OnUnlock;

        GetNode<UpgradeInfoContainer>("UpgradeInfoContainer").SetUpgrade(tractorSpeedUpgrade.GetInfo(),
            tractorSpeedUpgrade.GetCost(),
            ref tractorSpeedUpgrade.OnCostChanged);

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        tractorSpeedUpgrade.Buy();

        tractorSpeedUpgrade.OnUnlock -= OnUnlock;

    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
