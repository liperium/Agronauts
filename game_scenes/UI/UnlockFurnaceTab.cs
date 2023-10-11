using Godot;
using System;

public partial class UnlockFurnaceTab : UnlockableTab
{
    protected override BaseIdleUpgrade GetUpgrade()
    {
        return GameState.instance.upgrades.unlockFurnaceUpgrade;
    }
}
