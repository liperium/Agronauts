using Godot;
using System;

public partial class FurnaceSpeedUpgradeUI : HBoxContainer
{
    private FurnaceSpeedUpgrade furnaceSpeedUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        furnaceSpeedUpgrade = GameState.instance.upgrades.furnaceSpeedUpgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        Visible = furnaceSpeedUpgrade.IsUnlocked();
        furnaceSpeedUpgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(furnaceSpeedUpgrade.GetInfo(),
            furnaceSpeedUpgrade.GetCost());
        furnaceSpeedUpgrade.SetOnCostChanged((value) => infoContainer.UpdateCostText(value));

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        furnaceSpeedUpgrade.Buy();

        furnaceSpeedUpgrade.ResetOnUnlock(OnUnlock);
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
