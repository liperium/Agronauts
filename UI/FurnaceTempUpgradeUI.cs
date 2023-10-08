using Godot;
using System;

public partial class FurnaceTempUpgradeUI : HBoxContainer
{
    private FurnaceTempUpgrade furnaceTempUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        furnaceTempUpgrade = GameState.instance.upgrades.furnaceTempUpgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        Visible = furnaceTempUpgrade.IsUnlocked();
        furnaceTempUpgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(furnaceTempUpgrade.GetInfo(),
            furnaceTempUpgrade.GetCost());
        furnaceTempUpgrade.SetOnCostChanged((value) => infoContainer.UpdateCostText(value));

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        furnaceTempUpgrade.Buy();

        furnaceTempUpgrade.ResetOnUnlock(OnUnlock);
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}