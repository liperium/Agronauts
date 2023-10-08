using Godot;
using System;

public partial class AlienFertilizerArtifactUI : HBoxContainer
{
    private AlienFertilizerArtifact alienFertilizerArtifact;
    public override void _Ready()
    {
        base._Ready();

        alienFertilizerArtifact = GameState.instance.upgrades.alienFertilizerArtifact;

        if (alienFertilizerArtifact.acquired)
        {
            QueueFree();
            return;
        }

        Visible = alienFertilizerArtifact.IsUnlocked();
        alienFertilizerArtifact.SetOnUnlock(OnUnlock);

        UpgradeInfoContainer infoContainer = GetNode<UpgradeInfoContainer>("UpgradeInfoContainer");
        infoContainer.SetUpgrade(alienFertilizerArtifact.GetInfo(),
            alienFertilizerArtifact.GetCost());

    }


    private void OnUnlock()
    {
        Visible = true;
    }
}
