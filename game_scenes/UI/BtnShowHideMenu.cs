using Godot;

public partial class BtnShowHideMenu : BaseButton
{
    [Export] AnimationTree animTree;

    public static BtnShowHideMenu instance = null;

    private bool opened;
    public override void _Ready()
    {
        base._Ready();

        instance = this;

        if (GameState.instance.numbers.potatoCount.GetValue() == 0)
        {
            Disabled = true;
            GetParent<TextureRect>().Hide();
        }

        GameState.instance.numbers.potatoCount.SetOnValueChanged(OnGetPotato);
    }

    public bool IsOpened()
    {
        return opened;
    }

    public override void _Pressed()
    {
        base._Pressed();

        opened = !opened;

        GetParent<TextureRect>().FlipH = !opened;
        
        animTree.Set("parameters/conditions/open", opened);
        animTree.Set("parameters/conditions/close", !opened);
    }

    private void OnGetPotato(long newValue)
    {
        if (newValue > 0)
        {
            GetParent<TextureRect>().Show();
            Disabled = false;
            
            GameState.instance.numbers.potatoCount.ResetOnValueChanged(OnGetPotato);
        }
    }
}
