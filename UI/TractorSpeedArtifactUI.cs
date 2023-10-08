using Godot;
using System;

public partial class TractorSpeedArtifactUI : HBoxContainer
{
    private TractorSpeedArtifact tractorSpeedArtifact;
    public override void _Ready()
    {
        base._Ready();

        tractorSpeedArtifact = GameState.instance.upgrades.tractorSpeedArtifact;

        if (tractorSpeedArtifact.acquired)
        {
            QueueFree();
            return;
        }

        Visible = tractorSpeedArtifact.IsUnlocked();
        tractorSpeedArtifact.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(tractorSpeedArtifact.GetInfo(),
            tractorSpeedArtifact.GetCost());

    }


    private void OnUnlock()
    {
        Visible = true;
    }
}
