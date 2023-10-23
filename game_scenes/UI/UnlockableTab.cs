using Godot;

public partial class UnlockableTab : Tab
{
    private BaseIdleUpgrade upgrade;

    [Export] public string popupTitle;
    [Export] public string popupDescription;

    public override void _Ready()
    {
        base._Ready();

        upgrade = GetUpgrade();
        if (upgrade == null)
        {
            GD.PrintErr($"Upgrade for unlockable tab {GetType()} is null! Please override GetUpgrade and set a non-null upgrade.");
            QueueFree();
            return;
        }

        if (upgrade.IsUnlocked())
        {
            Show();
        }
        else
        {
            Hide();
            upgrade.SetOnUnlock(UnlockTab);
        }
    }

    protected virtual BaseIdleUpgrade GetUpgrade()
    {
        return null;
    }

    private void UnlockTab()
    {
        upgrade.ResetOnUnlock(UnlockTab);
        
        Show();
        GamePopUp.instance.AddToQueue(new GamePopUpInfo(popupTitle,popupDescription));
    }
}