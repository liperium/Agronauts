using Godot;
using System;

public partial class AutoFurnaceUpgradeUI : HBoxContainer
{
    private AutoFurnaceUpgrade autoFurnaceUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        autoFurnaceUpgrade = GameState.instance.upgrades.autoFurnaceUpgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        if (autoFurnaceUpgrade.acquired)
        {
            QueueFree();
            return;
        }
        
        Visible = autoFurnaceUpgrade.IsUnlocked();
        autoFurnaceUpgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(autoFurnaceUpgrade.GetInfo(),
            autoFurnaceUpgrade.GetCost());

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        if (!autoFurnaceUpgrade.CanBuy()) return;
        autoFurnaceUpgrade.Buy();

        autoFurnaceUpgrade.ResetOnUnlock(OnUnlock);
        buyButton.Pressed -= PressBuy;

        QueueFree();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
