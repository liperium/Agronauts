using Godot;
using System;

public partial class UpgradeInfoContainer : Node
{
    [Export] public TextureRect upgradeImage;
    [Export] public RichTextLabel upgradeTitle;
    [Export] public RichTextLabel upgradeDescription;
    [Export] public RichTextLabel buyButtonText;

    public void SetUpgrade(InfoUpgrade info, long cost, ref Action<long> onCostChanged)
    {
        if (upgradeImage != null && info.GetImagePath() != "") upgradeImage.Texture = ResourceLoader.Load<CompressedTexture2D>(info.GetImagePath());
        if (upgradeTitle != null) upgradeTitle.Text = info.GetName();
        if (upgradeTitle != null) upgradeDescription.Text = info.GetDescription();
        UpdateCostText(cost);

        onCostChanged += UpdateCostText;
    }

    private void UpdateCostText(long newCost)
    {
        if (buyButtonText != null)
        {
            buyButtonText.Text = $"{Tr("KBUY")} ({newCost.FormattedNumber()} {Tr("KPOTATOES")})";
        }
    }
}