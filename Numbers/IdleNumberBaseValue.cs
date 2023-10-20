public class IdleNumberBaseValue : IdleNumber
{
    private long baseValue;

    public void SetBaseValue(long value)
    {
        baseValue = value;
    }

    public override void OnLoad()
    {
        base.OnLoad();
        value = baseValue;
        UpdateValue();
    }
    
}