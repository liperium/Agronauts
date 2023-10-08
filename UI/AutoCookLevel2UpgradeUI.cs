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

        Visible = autoCookLevel2Upgrade.IsUnlocked();
        autoCookLevel2Upgrade.OnUnlock += OnUnlock;

        GetNode<UpgradeInfoContainer>("UpgradeInfoContainer").SetUpgrade(autoCookLevel2Upgrade.GetInfo(),
            autoCookLevel2Upgrade.GetCost(),
            ref autoCookLevel2Upgrade.OnCostChanged);

        buyButton.Pressed += PressBuy;
    }

    private void PressBuy()
    {
        if (!autoCookLevel2Upgrade.CanBuy()) return;
        autoCookLevel2Upgrade.Buy();

        autoCookLevel2Upgrade.OnUnlock -= OnUnlock;
        buyButton.Pressed -= PressBuy;

        QueueFree();
    }

    private void OnUnlock()
    {
        Visible = true;
    }
}
