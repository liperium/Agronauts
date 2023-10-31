using Godot;
using WJA23Godot.GameState;
using WJA23Godot.Upgrades;

public partial class FightWinScreen : Control
{
    [Export] public Container artifactsContainer;
    [Export] public PackedScene artifactTemplate;

    public void AddDroppedArtifact(IArtifact artifact)
    {
        if (artifactTemplate == null)
        {
            GD.PrintErr("Artifact template is null!");
            return;
        }

        UpgradeHolderUI template = artifactTemplate.Instantiate<UpgradeHolderUI>();
        template.NoButton();
        template.NoEffectText();
        template.Init(artifact);

        if (artifactsContainer == null)
        {
            GD.PrintErr("Artifact container is null!");
            return;
        }
        
        artifactsContainer.AddChild(template);
    }
}
