using Godot;
using System;

public partial class Tab : TextureButton
{
    [Export] public int tabIndex;
    [Export] public TabContainer tabContainer;
    [Export] public TextureRect bg;
    [Export] public Texture bgTexture;
    public override void _Pressed()
    {
        base._Pressed();
        if (tabContainer != null)
        {
            tabContainer.CurrentTab = tabIndex;
        }
    }
}
