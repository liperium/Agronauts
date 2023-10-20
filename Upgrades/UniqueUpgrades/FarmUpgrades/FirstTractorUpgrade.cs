using Godot;
using System;
[Serializable]
public partial class FirstTractorUpgrade : BuyableUpgrade<MultiplierModifier>
{
    public override void OnBuy()
    {
        SpawnTractor();
        GameState.instance.numbers.truckAmount.IncreaseValue(1);
        GamePopUp.instance.AddToQueue(new GamePopUpInfo("KCONGRATULATIONS","KCONTROLTRACTOR"));
        base.OnBuy();
    }

    public void SpawnTractor()
    {
        Tracteur tractor = ResourceLoader.Load<PackedScene>("res://game_scenes/farm/tracteur/tracteur.tscn").Instantiate() as Tracteur;
        ObjectSpawner.Spawn(tractor, new Vector2(640,360));
    }

    public override void UpdateCost()
    {
        cost = 100;
    }

    public override IdleNumber GetAffectedNumber()
    {
        return GameState.instance.numbers.potatoYield;
    }

    public override void Apply()
    {
        return;
    }

    public override void InitInfo()
    {
        base.InitInfo();
        info.SetName("KFIRSTTRACTORUPGRADE");
        info.SetDescription("KFIRSTTRACTORUPGRADEDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/tracteurmanuel.png");
    }

    public override void OnLoad()
    {
        base.OnLoad();

        if (IsUnlocked() == false)
        {
            GameState.instance.numbers.numberOfTilesUnlocked.SetOnValueChanged(CheckUnlock);
        }
    }

    public void CheckUnlock(long tiles)
    {
        if(tiles >= 10)
        {
            Unlock();
            GameState.instance.numbers.numberOfTilesUnlocked.ResetOnValueChanged(CheckUnlock);
        }
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Farm;
    }
}
