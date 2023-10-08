using Godot;
using System;

public partial class FurnaceSpeedArtifactUI : HBoxContainer
{
    private FurnaceSpeedArtifact furnaceSpeedArtifact;
    public override void _Ready()
    {
        base._Ready();

        furnaceSpeedArtifact = GameState.instance.upgrades.furnaceSpeedArtifact;

        if (furnaceSpeedArtifact.acquired)
        {
            QueueFree();
            return;
        }

        Visible = furnaceSpeedArtifact.IsUnlocked();
        furnaceSpeedArtifact.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(furnaceSpeedArtifact.GetInfo(),
            furnaceSpeedArtifact.GetCost(), furnaceSpeedArtifact.GetEffectText());

    }


    private void OnUnlock()
    {
        Visible = true;
    }
}
