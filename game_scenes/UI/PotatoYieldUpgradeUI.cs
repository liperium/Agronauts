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
        
        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(potatoYieldUpgrade.GetInfo(),
            potatoYieldUpgrade.GetCost(), potatoYieldUpgrade.GetEffectText());
        potatoYieldUpgrade.SetOnCostChanged((value) => { 
            infoContainer.UpdateCostText(value);
            infoContainer.SetUpgrade(potatoYieldUpgrade.GetInfo(),
            potatoYieldUpgrade.GetCost(), potatoYieldUpgrade.GetEffectText());
        });

        buyButton = GetNode<TextureButton>("BuyButton");
        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        potatoYieldUpgrade.Buy();
    }
}
