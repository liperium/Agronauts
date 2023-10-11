using Godot;

public partial class UpgradeHolderUI : Control
{
    private IBuyable genericUpgrade;
    private Button buyButton;
    private RichTextLabel buyButtonText;
    private TextureRect upgradeImage;
    private Label upgradeTitle;
    private Label upgradeDescription;

    private bool onUnlockPlugged;

    public void Init(IBuyable upgrade)
    {
        buyButton = GetNode<Button>("HBoxContainer/AspectRatioContainer/Button");
        buyButtonText = GetNode<RichTextLabel>("HBoxContainer/AspectRatioContainer/Button/CenterContainer/RichTextLabel");
        buyButton.Pressed += PressBuy;

        upgradeTitle = GetNode<Label>("HBoxContainer/VBoxContainer/HBoxContainer/UpTitle");
        upgradeImage = GetNode<TextureRect>("HBoxContainer/VBoxContainer/HBoxContainer/Icon");
        upgradeDescription = GetNode<Label>("HBoxContainer/VBoxContainer/UpDesc");

        genericUpgrade = upgrade;
        Name = genericUpgrade.GetInfo().GetName();

        SetUpgrade(genericUpgrade.GetInfo(),
            genericUpgrade.GetCost(), genericUpgrade.GetEffectText());
        
        genericUpgrade.SetOnCostChanged(OnUpgradeCostChanged);
    }

    public override void _Ready()
    {
        base._Ready();
        bool unlocked = genericUpgrade.IsUnlocked();
                
        if (unlocked == false)
        {
            Hide();
            genericUpgrade.SetOnUnlock(OnUnlock);
            onUnlockPlugged = true;
        }
        else
        {
            Show();
        }
    }

    private void PressBuy()
    {
        genericUpgrade.Buy();
    }

    private void OnUnlock()
    {
        Show();
        genericUpgrade.ResetOnUnlock(OnUnlock);
        onUnlockPlugged = false;
    }
    
    private void OnUpgradeCostChanged(long cost)
    {
        SetUpgrade(genericUpgrade.GetInfo(), cost, genericUpgrade.GetEffectText());
    }

    private void SetUpgrade(InfoUpgrade info, long cost, string effect)
    {
        if (upgradeImage != null && info.GetImagePath() != "") upgradeImage.Texture = ResourceLoader.Load<CompressedTexture2D>(info.GetImagePath());
        if (upgradeTitle != null) upgradeTitle.Text = Tr(info.GetName()) +" "+ effect;
        if (upgradeDescription != null) upgradeDescription.Text = info.GetDescription();
        UpdateCostText(cost,info.GetCostImagePath());
    }

    private void UpdateCostText(long newCost, string imagePath)
    {
        if (buyButtonText != null)
        {
            buyButtonText.Text = $"[center]{newCost.FormattedNumber()}[img=20xz20]{imagePath}[/img][/center]";
        }
    }

    public void NoButton()
    {
        GetNode<Control>("HBoxContainer/AspectRatioContainer").Visible = false;
    }
    public void FreeMe()
    {
        genericUpgrade.ResetOnBuyUpgrade(FreeMe);
        QueueFree();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        genericUpgrade.SetOnCostChanged(OnUpgradeCostChanged);
        
        if (onUnlockPlugged)
        {
            genericUpgrade.ResetOnUnlock(OnUnlock);
        }
    }
}