using Godot;
using System;

public partial class FirstTractorUpgrade : BuyableUpgrade<MultiplierModifier>
{

    public override void OnBuy()
    {
        Tracteur tractor = ResourceLoader.Load<PackedScene>("res://game_scenes/farm/tracteur/tracteur.tscn").Instantiate() as Tracteur;
        ObjectSpawner.Spawn(tractor, new Vector2(640,360));
        tractor.automatic = true;
        tractor.topLeftBound = tractor.Position;
        tractor.bottomRightBound = new Vector2(tractor.Position.X + FarmFieldMaster.TILE_PER_FF * FarmFieldMaster.TILE_SIZE, tractor.Position.Y + FarmFieldMaster.TILE_PER_FF * FarmFieldMaster.TILE_SIZE);
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
        if(GameState.instance.numbers.numberOfTilesUnlocked.GetValue() >= 10)
        {
            unlocked = true;
        }
        else
        {
            GameState.instance.numbers.numberOfTilesUnlocked.SetOnValueChanged(CheckUnlock);
        }
    }

    public void CheckUnlock(long tiles)
    {
        if(tiles >= 10)
        {
            unlocked = true;
        }
    }
}
