using Godot;
using System;

public partial class UnlockArtifactsTab : Control
{
    private bool subscribed = false;
    public override void _Ready()
    {
        base._Ready();

        GetParent<TextureButton>().Visible = false;
        
        if (GameState.instance.upgrades.unlockArtifactsUpgrade.IsUnlocked())
        {
            GetParent<TextureButton>().Visible = true;
        }
        else
        {
            GameState.instance.upgrades.unlockArtifactsUpgrade.SetOnUnlock(UnlockTab);
            subscribed = true;
        }
    }

    private void UnlockTab()
    {
        GameState.instance.upgrades.unlockArtifactsUpgrade.ResetOnUnlock(UnlockTab);
        subscribed = false;
        GetParent<TextureButton>().Visible = true;
        
        unlock_pop_up.instance.ChangeText("KUNLOCKARTIFACTS", "KUNLOCKARTIFACTSDESC");
        unlock_pop_up.instance.Animation();
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        if (subscribed)
        {
            GameState.instance.upgrades.unlockArtifactsUpgrade.ResetOnUnlock(UnlockTab);
            subscribed = false;
        }
        
    }
}
