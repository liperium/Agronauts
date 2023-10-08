using Godot;
using System;

public partial class AddAutoTractorUpgradeUI : HBoxContainer
{
    private AddAutomaticTractorUpgrade addAutomaticTractorUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        addAutomaticTractorUpgrade = GameState.instance.upgrades.addAutomaticTractorUpgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        Visible = addAutomaticTractorUpgrade.IsUnlocked();
        addAutomaticTractorUpgrade.OnUnlock += OnUnlock;

        GetNode<UpgradeInfoContainer>("UpgradeInfoContainer").SetUpgrade(addAutomaticTractorUpgrade.GetInfo(),
            addAutomaticTractorUpgrade.GetCost(),
            ref addAutomaticTractorUpgrade.OnCostChanged);

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        addAutomaticTractorUpgrade.Buy();

        addAutomaticTractorUpgrade.OnUnlock -= OnUnlock;
        
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
