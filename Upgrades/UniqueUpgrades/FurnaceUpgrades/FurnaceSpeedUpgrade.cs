using Godot;

public partial class FurnaceSpeedUpgrade : TieredUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        modifier.multiplier += 0.1f;
        base.OnBuy();
    }
    
    public override void UpdateModifier()
    {
        modifier.multiplier = 1 + 0.1f*tier;
    }

    public override void UpdateCost()
    {
        cost = (long)(300 + Mathf.Pow(1.2f, tier) + 5 * tier);
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.furnaceSpeed;
    }

    public override void InitInfo()
    {
        base.InitInfo();

        info.SetName("KFURNACESPEED");
        info.SetDescription("KFURNACESPEEDDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/fourSPEED.png");
    }

    public override void OnLoad()
    {
        base.OnLoad();

       if(IsUnlocked() == false) GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(CheckUnlock);
    }

    private void CheckUnlock(long value)
    {
        if (GameState.instance.numbers.cookedPotatoCount.GetValue() > 0)
        {
            Unlock();
            GameState.instance.numbers.cookedPotatoCount.ResetOnValueChanged(CheckUnlock);
        }
    }

    public override string GetEffectText()
    {
        return  + Mathf.RoundToInt((modifier.multiplier * 100)) + "%";
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Furnace;
    }
}
