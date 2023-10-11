using System;

[Serializable]
public class BaseIdleUpgrade
{
    protected InfoUpgrade info;

    public bool acquired;
    
    public bool unlocked;
    private Action OnUnlock;
    
    public void SetOnUnlock(Action action)
    {
        OnUnlock += action;
    }
	
    public void ResetOnUnlock(Action action)
    {
        OnUnlock -= action;
    }
    
    public void Unlock()
    {
        if (unlocked == false)
        {
            unlocked = true;
            if (OnUnlock != null) OnUnlock();
        }
    }
    
    public bool IsUnlocked()
    {
        return unlocked;
    }

    public virtual bool CanUnlock()
    {
        return true;
    }
    
    public InfoUpgrade GetInfo()
    {
        return info;
    }

    public virtual void InnitInfo()
    {
        info = new InfoUpgrade();
        info.SetName("ERROR");
        info.SetDescription("ERROR NOT SET");
        info.SetImagePath("res://theming/icon.svg");
    }
    
    public virtual void OnLoad()
    {
        InnitInfo();
    }
}