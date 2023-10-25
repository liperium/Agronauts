using Godot;

public partial class UpgradeHolderUI : Control
{
    private IBuyable genericUpgrade;
    private Button buyButton;
    private RichTextLabel buyButtonText;
    private TextureRect upgradeImage;
    private Label upgradeTitle;
    private Label upgradeDescription;
    private Label upgradeAddDescription;

    private Tab tab;
    private bool noEffectText;

    public void Init(IBuyable upgrade)
    {
        buyButton = GetNode<Button>("HBoxContainer/AspectRatioContainer/Button");
        buyButtonText = GetNode<RichTextLabel>("HBoxContainer/AspectRatioContainer/Button/CenterContainer/RichTextLabel");
        buyButton.Pressed += PressBuy;

        upgradeTitle = GetNode<Label>("HBoxContainer/VBoxContainer/HBoxContainer/UpTitle");
        upgradeImage = GetNode<TextureRect>("HBoxContainer/VBoxContainer/HBoxContainer/Icon");
        upgradeDescription = GetNode<Label>("HBoxContainer/VBoxContainer/UpDesc");
        upgradeAddDescription = GetNode<Label>("HBoxContainer/VBoxContainer/UpDescAdd");

        genericUpgrade = upgrade;
        Name = genericUpgrade.GetInfo().GetName();

        //set bg color
        this.ThemeTypeVariation = upgrade.GetBgStyle();
        UpdateAllInfo();
        
        //events
        genericUpgrade.SetOnCostChanged(OnUpgradeCostChanged);
        genericUpgrade.SetOnInfoChanged(UpdateAllInfo);

        if (ShowHideMenu.instance != null)
        {
            genericUpgrade.SetOnUnlock(ShowHideMenu.instance.UnlockedUpgrade);
        }

        if (genericUpgrade.GetUpgradeTab() != UIManager.UpgradeTab.Artifact)
        {
            if (genericUpgrade.IsMaxed())
            {
                FreeMe();
            }
            else 
            {
                genericUpgrade.SetOnMaxedUpgrade(FreeMe);
            }
        }

    }

    public override void _Ready()
    {
        base._Ready();
        
        if (genericUpgrade.IsMaxed() && genericUpgrade.GetUpgradeTab() != UIManager.UpgradeTab.Artifact)
        {
            ToggleShow(UIManager.AreAcquiredUpgradesShown());
            return;
        }
        
        if (genericUpgrade.IsUnlocked() == false)
        {
            Hide();
            genericUpgrade.SetOnUnlock(OnUnlock);
        }
        else
        {
            Show();
        }
    }

    public void SetTab(Tab holderTab)
    {
        tab = holderTab;
        genericUpgrade.SetOnUnlock(tab.FlashTab);
    }

    private void PressBuy()
    {
        genericUpgrade.Buy();
    }

    private void OnUnlock()
    {
        Show();
        genericUpgrade.ResetOnUnlock(OnUnlock);
    }
    
    private void OnUpgradeCostChanged(long cost)
    {
        SetUpgrade(genericUpgrade.GetInfo(), cost, genericUpgrade.GetEffectText());
    }

    private void UpdateAllInfo()
    {
        SetUpgrade(genericUpgrade.GetInfo(), genericUpgrade.GetCost(), genericUpgrade.GetEffectText());
    }

    private void SetUpgrade(InfoUpgrade info, long cost, string effect)
    {
        string effectText = noEffectText ? "" : $" {effect}";
        
        if (upgradeImage != null && info.GetImagePath() != "") upgradeImage.Texture = info.GetImage();
        if (upgradeTitle != null) upgradeTitle.Text = Tr(info.GetName()) + effectText;
        if (upgradeDescription != null) upgradeDescription.Text = info.GetDescription();
        if (upgradeAddDescription != null) upgradeAddDescription.Text = TranslateEntireString(info.GetAdditionalDescription());
        UpdateCostText(cost,info.GetCostImagePath());
    }

    private string TranslateEntireString(string toTranslate)
    {
        string result = "";
        foreach (string s in toTranslate.Split(' '))
        {
            result += Tr(s) + " ";
        }
        return result;
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
        GetNode<Control>("HBoxContainer/AspectRatioContainer").Hide();
    }

    public void NoEffectText()
    {
        noEffectText = true;
    }
    public void FreeMe()
    {
        genericUpgrade.ResetOnMaxedUpgrade(FreeMe);
        NoButton();
        UIManager.SetOnShowAcquired(ToggleShow);
        ToggleShow(UIManager.AreAcquiredUpgradesShown());
    }
    
    public void ToggleShow(bool show)
    {
        if (show) Show();
        else Hide();
    }
}