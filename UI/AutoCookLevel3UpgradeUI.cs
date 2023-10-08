using Godot;
using System;

public partial class AutoCookLevel3UpgradeUI : HBoxContainer
{
    private AutoCookLevel3Upgrade autoCookLevel3Upgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        autoCookLevel3Upgrade = GameState.instance.upgrades.autoCookLevel3Upgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        if (autoCookLevel3Upgrade.acquired)
        {
            QueueFree();
            return;
        }

        Visible = autoCookLevel3Upgrade.IsUnlocked();
        autoCookLevel3Upgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(autoCookLevel3Upgrade.GetInfo(),
            autoCookLevel3Upgrade.GetCost());

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        if (!autoCookLevel3Upgrade.CanBuy()) return;
        autoCookLevel3Upgrade.Buy();

        autoCookLevel3Upgrade.ResetOnUnlock(OnUnlock);
        buyButton.Pressed -= PressBuy;

        QueueFree();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
