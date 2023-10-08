using Godot;
using System;

public partial class TractorSpreadSeedsUpgradeUI : HBoxContainer
{
    private TractorSpreadSeedsUpgrade tractorSpreadSeedsUpgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        tractorSpreadSeedsUpgrade = GameState.instance.upgrades.tractorSpreadSeedsUpgrade;

        if (tractorSpreadSeedsUpgrade.acquired)
        {
            QueueFree();
            return;
        }

        Visible = tractorSpreadSeedsUpgrade.IsUnlocked();
        tractorSpreadSeedsUpgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(tractorSpreadSeedsUpgrade.GetInfo(),
            tractorSpreadSeedsUpgrade.GetCost(), tractorSpreadSeedsUpgrade.GetEffectText());

        buyButton = GetNode<TextureButton>("BuyButton");
        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        if (!tractorSpreadSeedsUpgrade.CanBuy()) return;

        tractorSpreadSeedsUpgrade.Buy();

        tractorSpreadSeedsUpgrade.ResetOnUnlock(OnUnlock);
        buyButton.Pressed -= PressBuy;

        QueueFree();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
