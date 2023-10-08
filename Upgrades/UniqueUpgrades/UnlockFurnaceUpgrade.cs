
public class UnlockFurnaceUpgrade : BuyableUpgrade<IdleModifier>
{
    public override void OnLoad()
    {
        if (IsUnlocked() == false) GameState.instance.upgrades.totalPotatoYieldUpgrade.SetOnBuyUpgrade(OnBuyPotatoYieldUpgrade);
        base.OnLoad();
    }

    private void OnBuyPotatoYieldUpgrade()
    {
        if (GameState.instance.upgrades.totalPotatoYieldUpgrade.tier >= 5)
        {
            Unlock();
            Buy();
        }   
    }

    public override void UpdateCost()
    {
        cost = 0;
    }
}