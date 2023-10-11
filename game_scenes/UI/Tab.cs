using Godot;
using System;

public partial class Tab : Control
{
    [Export] public int tabIndex;
    [Export] public TabContainer tabContainer;

    private AnimationTree animationTree;
    private BaseButton button;
    public override void _Ready()
    {
        base._Ready();
        animationTree = GetNode<AnimationTree>("AnimationTree");
        button = GetNode<BaseButton>("Button");

        button.Pressed += TabButtonPressed;
        //TODO unlock certain tabs
        FlashTab();
    }

    public void TabButtonPressed()
    {
        if (tabContainer != null)
        {
            animationTree.Set("parameters/conditions/idle", true);
            animationTree.Set("parameters/conditions/flash", false);
            tabContainer.CurrentTab = tabIndex;
        }
        else
        {
            GD.PrintErr("TabContainer empty on tab button");
        }
    }

    public void FlashTab()
    {
        if (tabContainer != null)
        {
            animationTree.Set("parameters/conditions/flash", true);
            animationTree.Set("parameters/conditions/idle", false);
        }
        else
        {
            GD.PrintErr("TabContainer empty on tab button");
        }
    }
}
