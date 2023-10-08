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
        tractorSpeedUpgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(tractorSpeedUpgrade.GetInfo(),
            tractorSpeedUpgrade.GetCost());
        tractorSpeedUpgrade.SetOnCostChanged((value) => infoContainer.UpdateCostText(value));
        
        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        tractorSpeedUpgrade.Buy();

        tractorSpeedUpgrade.ResetOnUnlock(OnUnlock);

    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
