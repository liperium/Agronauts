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
        addAutomaticTractorUpgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(addAutomaticTractorUpgrade.GetInfo(),
            addAutomaticTractorUpgrade.GetCost(), addAutomaticTractorUpgrade.GetEffectText());
        addAutomaticTractorUpgrade.SetOnCostChanged((value) => {
            infoContainer.UpdateCostText(value);
            infoContainer.SetUpgrade(addAutomaticTractorUpgrade.GetInfo(),
                        addAutomaticTractorUpgrade.GetCost(), addAutomaticTractorUpgrade.GetEffectText());
        });

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        addAutomaticTractorUpgrade.Buy();

        addAutomaticTractorUpgrade.ResetOnUnlock(OnUnlock);
    }

    private void OnUnlock()
    {
        Visible = true;
    }


}
