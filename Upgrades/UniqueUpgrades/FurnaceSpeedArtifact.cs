using Godot;
using System;

public partial class FurnaceSpeedArtifact : ArtifactUpgrade<MultiplierModifier>
{
    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.furnaceSpeed;
    }

    public override void UpdateModifier()
    {
        modifier.multiplier = 1 + Mathf.RoundToInt( 0.1f * tier);
    }

    public override void UpdateCost()
    {
        cost = 0;
    }


    public override void InitInfo()
    {
        base.InitInfo();
        info.SetName("KFURNACESPEEDARTIFACT");
        info.SetDescription("KFURNACESPEEDARTIFACTDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/CarburantInterstellaire.png");
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Artifact;
    }
}
