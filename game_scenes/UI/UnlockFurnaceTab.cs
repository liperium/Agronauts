using Godot;
using System;

public partial class UnlockFurnaceTab : Control
{
    public override void _Ready()
    {
        base._Ready();

        GetParent<TextureButton>().Visible = false;
        
        if (GameState.instance.upgrades.unlockFurnaceUpgrade.IsUnlocked())
        {
            GetParent<TextureButton>().Visible = true;
        }
        else
        {
            GameState.instance.upgrades.unlockFurnaceUpgrade.SetOnUnlock(UnlockTab);
        }
    }

    private void UnlockTab()
    {
        GameState.instance.upgrades.unlockFurnaceUpgrade.ResetOnUnlock(UnlockTab);
        
        GetParent<TextureButton>().Visible = true;
        
        unlock_pop_up.instance.ChangeText("KUNLOCKFURNACE", "KUNLOCKFURNACEDESC");
        unlock_pop_up.instance.Animation();
    }
}
