using Godot;
using System;

public partial class Tab : Control
{
    [Export] public int tabIndex;
    [Export] public TabContainer tabContainer;

    private BaseButton button;
    public override void _Ready()
    {
        base._Ready();
        button = GetNode<BaseButton>("Button");

        button.Pressed += TabButtonPressed;
        //FlashTab();
    }

    public void TabButtonPressed()
    {
        if (tabContainer != null)
        {
            GetNode<Flash>("Flash").Stop();
            tabContainer.CurrentTab = tabIndex;
        }
        else
        {
            GD.PrintErr("TabContainer empty on tab button");
        }
    }

    public void FlashTab()
    {
        if (tabContainer != null && (tabContainer.CurrentTab != tabIndex || !ShowHideMenu.instance.IsOpened()))
        {
            GetNode<Flash>("Flash").Start();
        }
        else
        {
            GD.PrintErr("TabContainer empty on tab button");
        }
    }
}
