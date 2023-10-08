using Godot;
using System;

public partial class UpgradeInfoContainer : Node
{
    [Export] public TextureRect upgradeImage;
    [Export] public RichTextLabel upgradeTitle;
    [Export] public RichTextLabel upgradeDescription;
    [Export] public RichTextLabel buyButtonText;

    public void SetUpgrade(InfoUpgrade info, long cost, string effect)
    {
        if (upgradeImage != null && info.GetImagePath() != "") upgradeImage.Texture = ResourceLoader.Load<CompressedTexture2D>(info.GetImagePath());
        if (upgradeTitle != null) upgradeTitle.Text = Tr(info.GetName()) +" "+ effect;
        if (upgradeDescription != null) upgradeDescription.Text = info.GetDescription();
        UpdateCostText(cost);
    }

    public void UpdateCostText(long newCost)
    {
        if (buyButtonText != null)
        {
            buyButtonText.Text = $"[center]{newCost.FormattedNumber()}[img=20xz20]res://Icons/Potato.png[/img][/center]";
        }
    }
}