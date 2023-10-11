using Godot;

public partial class UnlockableTab : Tab
{
    private bool onUnlockPlugged;

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
            onUnlockPlugged = true;
        }
    }

    protected virtual BaseIdleUpgrade GetUpgrade()
    {
        return null;
    }

    private void UnlockTab()
    {
        upgrade.ResetOnUnlock(UnlockTab);
        onUnlockPlugged = false;
        
        Show();
        unlock_pop_up.instance.ChangeText(popupTitle, popupDescription);
        unlock_pop_up.instance.Animation();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        if (onUnlockPlugged)
        {
            upgrade.ResetOnUnlock(UnlockTab);
            onUnlockPlugged = false;
        }
    }
}