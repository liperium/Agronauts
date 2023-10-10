using Godot;
using System;

public partial class UnlockFurnaceTab : Control
{
    private bool onUnlockPlugged = false;
    
    public override void _Ready()
    {
        base._Ready();
        GetParent<Control>().Visible = false;
        
        if (GameState.instance.upgrades.unlockFurnaceUpgrade.IsUnlocked())
        {
            GetParent<Control>().Visible = true;
        }
        else
        {
            GameState.instance.upgrades.unlockFurnaceUpgrade.SetOnUnlock(UnlockTab);
            onUnlockPlugged = true;
        }
    }

    private void UnlockTab()
    {
        GameState.instance.upgrades.unlockFurnaceUpgrade.ResetOnUnlock(UnlockTab);
        onUnlockPlugged = false;
        
        GetParent<Control>().Visible = true;
        unlock_pop_up.instance.ChangeText("KUNLOCKFURNACE", "KUNLOCKFURNACEDESC");
        unlock_pop_up.instance.Animation();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        if (onUnlockPlugged)
        {
            GameState.instance.upgrades.unlockFurnaceUpgrade.ResetOnUnlock(UnlockTab);
            onUnlockPlugged = false;
        }
    }
}
