using Godot;

public partial class NumberIndicator : Control
{
    [Export] public RichTextLabel label;
    public void SetNumber(long number)
    {
        if (label == null)
        {
            return;
        }

        label.Text = number.FormattedNumber();
    }
}
