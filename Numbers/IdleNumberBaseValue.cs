using System;

[Serializable]
public class IdleNumberBaseValue : IdleNumber
{
    public long baseValue;

    public void SetBaseValue(long value)
    {
        baseValue = value;
    }

    public long GetBaseValue()
    {
        return baseValue;
    }

    public override void OnLoad()
    {
        base.OnLoad();
        SetValue(baseValue, false);
        UpdateValue();
    }
    
}