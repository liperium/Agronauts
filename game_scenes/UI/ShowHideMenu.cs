using Godot;

public partial class ShowHideMenu : TextureRect
{
    [Export] AnimationTree animTree;

    public static ShowHideMenu instance = null;

    public BaseButton button = null;
    public Flash flash = null;

    private bool opened;
    public override void _Ready()
    {
        base._Ready();

        instance = this;

        button = GetNode<BaseButton>("Button");
        flash = GetNode<Flash>("Flash");

        if (GameState.instance.numbers.potatoCount.GetValue() == 0)
        {
            button.Disabled = true;
            Hide();
        }

        GameState.instance.numbers.potatoCount.SetOnValueChanged(OnGetPotato);
        button.Pressed += ButtonPressed;
    }

    public bool IsOpened()
    {
        return opened;
    }

    private void ButtonPressed()
    {
        opened = !opened;

        FlipH = !opened;
        
        animTree.Set("parameters/conditions/open", opened);
        animTree.Set("parameters/conditions/close", !opened);
        flash.Stop();
    }

    private void OnGetPotato(long newValue)
    {
        if (newValue > 0)
        {
            Show();
            button.Disabled = false;
            
            GameState.instance.numbers.potatoCount.ResetOnValueChanged(OnGetPotato);
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("toggle_left_menu"))
        {
            button.FocusMode = FocusModeEnum.Click;
            ButtonPressed();
        }
    }

    public void UnlockedUpgrade()
    {
        if (!opened)
        {
            flash.Start();
        }
    }
}
