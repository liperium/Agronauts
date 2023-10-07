using Godot;

public partial class BtnShowHideMenu : TextureButton
{
    [Export] AnimationTree animTree;

    private bool opened;
    public override void _Ready()
    {
        base._Ready();
        
        animTree.AnimationFinished += name =>
        {
            Disabled = false;
        };
    }

    public override void _Pressed()
    {
        base._Pressed();
        
        GD.Print("Menu Open/Close");

        opened = !opened;
        
        animTree.Set("parameters/conditions/open", opened);
        animTree.Set("parameters/conditions/close", !opened);
        
        //this.Disabled = true;

    }
}
