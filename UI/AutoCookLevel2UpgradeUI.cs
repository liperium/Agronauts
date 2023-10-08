using Godot;
using System;

public partial class AutoCookLevel2UpgradeUI : HBoxContainer
{
    private AutoCookLevel2Upgrade autoCookLevel2Upgrade;
    private TextureButton buyButton;
    public override void _Ready()
    {
        base._Ready();

        autoCookLevel2Upgrade = GameState.instance.upgrades.autoCookLevel2Upgrade;
        buyButton = GetNode<TextureButton>("BuyButton");

        if (autoCookLevel2Upgrade.acquired)
        {
            QueueFree();
            return;
        }
        
        Visible = autoCookLevel2Upgrade.IsUnlocked();
        autoCookLevel2Upgrade.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(autoCookLevel2Upgrade.GetInfo(),
            autoCookLevel2Upgrade.GetCost());

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        if (!autoCookLevel2Upgrade.CanBuy()) return;
        autoCookLevel2Upgrade.Buy();

        autoCookLevel2Upgrade.ResetOnUnlock(OnUnlock);
        buyButton.Pressed -= PressBuy;

        QueueFree();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
