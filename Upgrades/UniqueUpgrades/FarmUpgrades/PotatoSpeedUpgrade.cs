
using Godot;

public class PotatoSpeedUpgrade : CappedTieredUpgrade<MultiplierModifier>
{
    public override void UpdateModifier()
    {
        modifier.multiplier = 1 + 0.3f*tier;
    }

    public override void UpdateCost()
    {
        cost = (long)(50 + Mathf.Pow(tier, 2.5f) + 5*tier);
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.potatoGrowSpeed;
    }

    public override void InitInfo()
    {
        base.InitInfo();
        info.SetName("KPOTATOSPEEDUPGRADE");
        info.SetDescription("KPOTATOSPEEDUPGRADEDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/EngraisNaturel.png");
    }

    public override long GetTierCap()
    {
        return 10;
    }

    public override void OnLoad()
    {
        base.OnLoad();
        if (IsUnlocked() == false) GameState.instance.numbers.potatoCount.SetOnValueChanged(CheckUnlock);
    }

    private void CheckUnlock(long value)
    {
        if (GameState.instance.numbers.potatoCount.GetValue() > 100)
        {
            Unlock();
            GameState.instance.numbers.potatoCount.ResetOnValueChanged(CheckUnlock);
        }   
    }

    public override string GetEffectText()
    {
        return "" + base.GetEffectText() + Mathf.RoundToInt(modifier.multiplier * 100) + "%";
    }

    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Farm;
    }
}