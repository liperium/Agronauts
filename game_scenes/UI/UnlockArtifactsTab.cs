using Godot;
using System;

public partial class UnlockArtifactsTab : UnlockableTab
{
    protected override BaseIdleUpgrade GetUpgrade()
    {
        return GameState.instance.upgrades.unlockArtifactsUpgrade;
    }
}
