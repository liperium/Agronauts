using Godot;
using System;

public partial class FirstTractorUpgrade : BuyableUpgrade<MultiplierModifier>
{

    public override void OnBuy()
    {
        Tracteur tractor = ResourceLoader.Load<PackedScene>("res://game_scenes/farm/tracteur/tracteur.tscn").Instantiate() as Tracteur;
        ObjectSpawner.Spawn(tractor, new Vector2(FarmFieldMaster.centerPos,FarmFieldMaster.centerPos)*FarmFieldMaster.TILE_SIZE);
        base.OnBuy();
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

    public override void InnitInfo()
    {
        base.InnitInfo();
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
}
