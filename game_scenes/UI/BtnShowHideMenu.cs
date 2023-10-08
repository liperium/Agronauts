using Godot;

public partial class BtnShowHideMenu : TextureButton
{
    [Export] AnimationTree animTree;

    private bool opened;
    public override void _Ready()
    {
        base._Ready();

        if (GameState.instance.numbers.potatoCount.GetValue() == 0)
        {
            Disabled = true;
            Visible = false;   
        }

        GameState.instance.numbers.potatoCount.SetOnValueChanged(OnGetPotato);
    }

    public override void _Pressed()
    {
        base._Pressed();
        
        GD.Print("Menu Open/Close");

        opened = !opened;

        FlipH = !opened;
        
        animTree.Set("parameters/conditions/open", opened);
        animTree.Set("parameters/conditions/close", !opened);
    }

    private void OnGetPotato(long newValue)
    {
        if (newValue > 0)
        {
            Visible = true;
            Disabled = false;
            
            GameState.instance.numbers.potatoCount.ResetOnValueChanged(OnGetPotato);
        }
    }
}
