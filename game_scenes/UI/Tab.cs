using Godot;
using System;

public partial class Tab : BaseButton
{
    [Export] public int tabIndex;
    [Export] public TabContainer tabContainer;
    public override void _Pressed()
    {
        base._Pressed();
        if (tabContainer != null)
        {
            tabContainer.CurrentTab = tabIndex;
        }
    }
}
