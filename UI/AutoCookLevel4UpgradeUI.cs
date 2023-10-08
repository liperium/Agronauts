using Godot;
using System;

public partial class AutoCookLevel4UpgradeUI : HBoxContainer
{
    private AutoCookLevel4Upgrade autoCookLevel4Upgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        autoCookLevel4Upgrade = GameState.instance.upgrades.autoCookLevel4Upgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        if (autoCookLevel4Upgrade.acquired)
        {
            QueueFree();
            return;
        }

        Visible = autoCookLevel4Upgrade.IsUnlocked();
        autoCookLevel4Upgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(autoCookLevel4Upgrade.GetInfo(),
            autoCookLevel4Upgrade.GetCost());

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        if (!autoCookLevel4Upgrade.CanBuy()) return;
        autoCookLevel4Upgrade.Buy();

        autoCookLevel4Upgrade.ResetOnUnlock(OnUnlock);
        buyButton.Pressed -= PressBuy;

        QueueFree();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
