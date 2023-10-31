using Godot;

public partial class NumberIndicator : Control
{
    [Export] public RichTextLabel label;
    private string format;
    public void SetNumber(long number)
    {
        if (label == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(format))
        {
            label.Text = number.FormattedNumber();
        }
        else
        {
            label.Text = string.Format(format, number.FormattedNumber());
        }
    }

    public void SetFormat(string format)
    {
        this.format = format;
    }
}
