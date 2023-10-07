using Godot;
using System;

public partial class UpgradeInfoContainer : HBoxContainer
{
    [Export] public TextureRect upgradeImage;
    [Export] public RichTextLabel upgradeTitle;
    [Export] public RichTextLabel upgradeDescription;
    [Export] public RichTextLabel buyButtonText;

    public void SetUpgrade(BuyableUpgrade<IdleModifier> upgrade)
    {
        InfoUpgrade info = upgrade.GetInfo();
        if (upgradeImage != null && info.GetImagePath() != "") upgradeImage.Texture = ResourceLoader.Load<CompressedTexture2D>(info.GetImagePath());
        if (upgradeTitle != null) upgradeTitle.Text = info.GetName();
        if (upgradeTitle != null) upgradeDescription.Text = info.GetDescription();
        UpdateCostText(upgrade.GetCost());

        upgrade.OnCostChanged += UpdateCostText;
    }

    private void UpdateCostText(long newCost)
    {
        if (buyButtonText != null) buyButtonText.Text = $"{Tr("KBUY")} ({newCost} {Tr("KPOTATOES")})";
    }
}