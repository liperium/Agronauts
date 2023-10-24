using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class BaseIdleUpgrade : ISaveable
{
    protected InfoUpgrade info;

    [JsonProperty]
    protected bool acquired;
    [JsonProperty]
    protected bool unlocked;
    
    private IdleAction OnUnlock;

    public BaseIdleUpgrade()
    {
        OnUnlock = new IdleAction();
    }

    public void SetOnUnlock(Action action)
    {
        OnUnlock += action;
    }
	
    public void ResetOnUnlock(Action action)
    {
        OnUnlock.RemoveManual(action);
    }
    
    public void Unlock()
    {
        if (unlocked == false)
        {
            unlocked = true;
            OnUnlock.Invoke();
        }
    }
    
    public bool IsUnlocked()
    {
        return unlocked;
    }
    
    /// <summary>
    /// Returns the text that will be appended to the upgrade title. Put things like caps, effects, etc.
    /// </summary>
    /// <returns>the string that will be appended to the upgrade title.</returns>
    public virtual string GetEffectText()
    {
        return "";
    }
    
    public InfoUpgrade GetInfo()
    {
        return info;
    }

    /// <summary>
    /// Initializes the information for this upgrade. You can set the name, description and image here. This needs to be overridden for each upgrade. You need to call base.
    /// </summary>
    public virtual void InitInfo()
    {
        info = new InfoUpgrade();
        info.SetName("ERROR");
        info.SetDescription("ERROR NOT SET");
        info.SetAdditionalDescription("");
        info.SetImagePath("res://theming/icon.svg");
    }
    
    public virtual void OnLoad()
    {
        InitInfo();
    }
}