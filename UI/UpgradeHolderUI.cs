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

    private bool onUnlockPlugged;
    private bool freeOnMaxPlugged;
    private bool toggleShowPlugged;

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
        this.AddThemeStyleboxOverride("panel", upgrade.GetBgStyle());
        
        UpdateAllInfo();
        
        //events
        genericUpgrade.SetOnCostChanged(OnUpgradeCostChanged);
        genericUpgrade.SetOnInfoChanged(UpdateAllInfo);
        
        genericUpgrade.SetOnUnlock(ShowHideMenu.instance.UnlockedUpgrade);

        if (genericUpgrade.GetUpgradeTab() != UIManager.UpgradeTab.Artifact)
        {
            if (genericUpgrade.IsMaxed())
            {
                FreeMe();
            }
            else 
            {
                genericUpgrade.SetOnMaxedUpgrade(FreeMe);
                freeOnMaxPlugged = true;
            }
        }

    }

    public override void _Ready()
    {
        base._Ready();
        
        if (genericUpgrade.IsMaxed())
        {
            ToggleShow(UIManager.AreAcquiredUpgradesShown());
            return;
        }
        
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
        onUnlockPlugged = false;
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
        if (upgradeImage != null && info.GetImagePath() != "") upgradeImage.Texture = info.GetImage();
        if (upgradeTitle != null) upgradeTitle.Text = Tr(info.GetName()) +" "+ effect;
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
    public void FreeMe()
    {
        genericUpgrade.ResetOnBuyUpgrade(FreeMe);
        NoButton();
        toggleShowPlugged = true;
        UIManager.SetOnShowAcquired(ToggleShow);
        ToggleShow(UIManager.AreAcquiredUpgradesShown());
    }
    
    public void ToggleShow(bool show)
    {
        if (show) Show();
        else Hide();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        genericUpgrade.ResetOnCostChanged(OnUpgradeCostChanged);
        
        if (onUnlockPlugged)
        {
            genericUpgrade.ResetOnUnlock(OnUnlock);
            onUnlockPlugged = false;
        }
        
        genericUpgrade.ResetOnInfoChanged(UpdateAllInfo);
        genericUpgrade.ResetOnUnlock(ShowHideMenu.instance.UnlockedUpgrade);

        if (freeOnMaxPlugged)
        {
            genericUpgrade.ResetOnMaxedUpgrade(FreeMe);
            freeOnMaxPlugged = false;
        }

        if (toggleShowPlugged)
        {
            UIManager.ResetOnShowAcquired(ToggleShow);
            toggleShowPlugged = false;
        }

        if (tab != null)
        {
            genericUpgrade.ResetOnUnlock(tab.FlashTab);
        }
    }
}