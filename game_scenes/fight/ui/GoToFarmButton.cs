using Godot;
using WJA23Godot.GameState;

public partial class GoToFarmButton : BaseButton
{
    private const string scenePath = "res://game_scenes/farm/farm.tscn";
    public override void _Pressed()
    {
        base._Pressed();
        
        SceneTransition.GoToScene(ResourceLoader.Load<PackedScene>(scenePath), GameScene.Farm);
    }
}