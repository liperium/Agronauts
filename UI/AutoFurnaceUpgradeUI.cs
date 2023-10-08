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

        Visible = autoFurnaceUpgrade.IsUnlocked();
        autoFurnaceUpgrade.OnUnlock += OnUnlock;

        GetNode<UpgradeInfoContainer>("UpgradeInfoContainer").SetUpgrade(autoFurnaceUpgrade.GetInfo(),
            autoFurnaceUpgrade.GetCost(),
            ref autoFurnaceUpgrade.OnCostChanged);

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        autoFurnaceUpgrade.Buy();

        autoFurnaceUpgrade.OnUnlock -= OnUnlock;
        buyButton.Pressed -= PressBuy;

        QueueFree();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
