using Godot;
using System;

public partial class PotatoYieldUpgradeUI : HBoxContainer
{
    private TotalPotatoYieldUpgrade potatoYieldUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        potatoYieldUpgrade = GameState.instance.upgrades.totalPotatoYieldUpgrade;

        buyButton = GetNode<TextureButton>("BuyButton");
        GetNode<UpgradeInfoContainer>("UpgradeInfoContainer").SetUpgrade(potatoYieldUpgrade.GetInfo(),
            potatoYieldUpgrade.GetCost(),
            ref potatoYieldUpgrade.OnCostChanged);

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        potatoYieldUpgrade.Buy();
    }
}
