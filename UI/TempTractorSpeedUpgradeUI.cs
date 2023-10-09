using Godot;
using System;

public partial class TempTractorSpeedUpgradeUI : HBoxContainer
{
    private TempTractorSpeedUpgrade tempTractorSpeedUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        tempTractorSpeedUpgrade = GameState.instance.upgrades.tempTractorSpeedUpgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        if (tempTractorSpeedUpgrade.acquired)
        {
            QueueFree();
            return;
        }

        Visible = tempTractorSpeedUpgrade.IsUnlocked();
        tempTractorSpeedUpgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(tempTractorSpeedUpgrade.GetInfo(),
            tempTractorSpeedUpgrade.GetCost(), tempTractorSpeedUpgrade.GetEffectText());

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        if (!tempTractorSpeedUpgrade.CanBuy()) return;
        tempTractorSpeedUpgrade.Buy();

        tempTractorSpeedUpgrade.ResetOnUnlock(OnUnlock);
        buyButton.Pressed -= PressBuy;

        QueueFree();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
