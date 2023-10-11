using Godot;
using System;

public partial class UnlockArtifactsTab : Control
{
    public override void _Ready()
    {
        base._Ready();

        GetParent<Control>().Hide();
        
        if (GameState.instance.upgrades.unlockArtifactsUpgrade.IsUnlocked())
        {
            GetParent<Control>().Show();
        }
        else
        {
            GameState.instance.upgrades.unlockArtifactsUpgrade.SetOnUnlock(UnlockTab);
        }
    }

    private void UnlockTab()
    {
        GameState.instance.upgrades.unlockArtifactsUpgrade.ResetOnUnlock(UnlockTab);
        
        GetParent<Control>().Show();
        
        unlock_pop_up.instance.ChangeText("KUNLOCKARTIFACTS", "KUNLOCKARTIFACTSDESC");
        unlock_pop_up.instance.Animation();
    }
}
