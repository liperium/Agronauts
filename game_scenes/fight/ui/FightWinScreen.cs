using Godot;
using WJA23Godot.Upgrades;

public partial class FightWinScreen : Control
{
    private PackedScene farmScene;
    
    [Export] public Container artifactsContainer;
    [Export] public BaseButton exitButton;
    [Export] public PackedScene artifactTemplate;

    public override void _Ready()
    {
        base._Ready();

        exitButton.Pressed += OnExitButtonPressed;
    }

    private void OnExitButtonPressed()
    {
        farmScene = ResourceLoader.Load<PackedScene>("res://game_scenes/farm/farm.tscn");
        SceneTransition.GoToScene(farmScene);
    }

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
