
using Godot;

public class PotatoSpeedUpgrade : TieredUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        modifier.multiplier += 0.3f;
        base.OnBuy();
    }

    public override void UpdateCost()
    {
        cost = (long)(50 + Mathf.Pow(2.5f, tier) + 5*tier);
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.potatoGrowSpeed;
    }

    public override void InnitInfo()
    {
        base.InnitInfo();
		
        info.SetName("KPOTATOSPEEDUPGRADE");
        info.SetDescription("KPOTATOSPEEDUPGRADEDESC");
        info.SetImagePath("res://Icons/Potato.png");
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
}