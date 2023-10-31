using Godot;
using System;

public partial class Tab : Control
{
    [Export] public int tabIndex;
    [Export] public TabContainer tabContainer;
    [Export] public Flash flash;

    private BaseButton button;
    public override void _Ready()
    {
        base._Ready();
        button = GetNode<BaseButton>("Button");
        flash = GetNode<Panel>("Panel").GetNode<Flash>("Flash");

        button.Pressed += TabButtonPressed;
        //FlashTab();
    }

    public void TabButtonPressed()
    {
        if (tabContainer != null)
        {
            flash.Stop();
            tabContainer.CurrentTab = tabIndex;
        }
    }

    public void FlashTab()
    {
        if (CanFlash())
        {
            flash.Start();
        }
    }

    public bool CanFlash()
    {
        return (tabContainer != null && (tabContainer.CurrentTab != tabIndex || !ShowHideMenu.instance.IsOpened()));
    }
}
