public partial class TractorSpreadSeedsUpgrade : BuyableUpgrade<IdleModifier>
{
    public override void UpdateCost()
    {
        cost = 100000;
    }

    public override void InitInfo()
    {
        base.InitInfo();
        info.SetName("KPLANTER");
        info.SetDescription("KPLANTERDESC");
        info.SetImagePath("res://game_scenes/farm/tracteur/sprites/epandeur.png");
    }

    public override void OnLoad()
    {
        base.OnLoad();

        if (IsUnlocked() == false)
        {
            GameState.instance.numbers.truckAmount.SetOnValueChanged(CheckUnlock);
        }
    }

    public void CheckUnlock(long truckAmount)
    {
        if (truckAmount >= 1)
        {
            Unlock();
            GameState.instance.numbers.truckAmount.ResetOnValueChanged(CheckUnlock);
        }
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Farm;
    }
}
