using Godot;
using System;

public partial class FurnaceBatchSizeUpgradeUI : HBoxContainer
{
    private FurnaceBatchSizeUpgrade furnaceBatchSizeUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        furnaceBatchSizeUpgrade = GameState.instance.upgrades.furnaceBatchSizeUpgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        Visible = furnaceBatchSizeUpgrade.IsUnlocked();
        furnaceBatchSizeUpgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(furnaceBatchSizeUpgrade.GetInfo(),
            furnaceBatchSizeUpgrade.GetCost(), furnaceBatchSizeUpgrade.GetEffectText());
        furnaceBatchSizeUpgrade.SetOnCostChanged((value) => {
            infoContainer.SetUpgrade(furnaceBatchSizeUpgrade.GetInfo(),
            furnaceBatchSizeUpgrade.GetCost(), furnaceBatchSizeUpgrade.GetEffectText());
        });

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        furnaceBatchSizeUpgrade.Buy();

        furnaceBatchSizeUpgrade.ResetOnUnlock(OnUnlock);
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
