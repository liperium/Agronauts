using Godot;
using System;
using System.Collections.Generic;

public partial class AddAutomaticTractorUpgrade : TieredUpgrade<MultiplierModifier>
{
	private List<FarmField> farmFieldsUnlocked;
	public void AddField(FarmField toAdd)
	{
        farmFieldsUnlocked.Add(toAdd);
	}

    public override void OnBuy()
    {

        FarmField farmField = farmFieldsUnlocked[0];
        farmFieldsUnlocked.RemoveAt(0);
        Tracteur tractor = ResourceLoader.Load<PackedScene>("res://game_scenes/farm/tracteur/tracteur.tscn").Instantiate() as Tracteur;
        ObjectSpawner.Spawn(tractor, new Vector2(farmField.GlobalPosition.X, farmField.GlobalPosition.Y));
        tractor.automatic = true;
        tractor.topLeftBound = tractor.Position;
        tractor.bottomRightBound = new Vector2(tractor.Position.X + FarmFieldMaster.TILE_PER_FF * FarmFieldMaster.TILE_SIZE, tractor.Position.Y + FarmFieldMaster.TILE_PER_FF * FarmFieldMaster.TILE_SIZE);
        GameState.instance.numbers.truckAmount.IncreaseValue(1);

        base.OnBuy();
    }

    public override void UpdateCost()
    {
        cost = 10000*(tier+1);
    }


    public override void InnitInfo()
    {
        base.InnitInfo();
        info.SetName("KADDAUTOTRACTORUPGRADE");
        info.SetDescription("KADDAUTOTRACTORUPGRADEDESC");
        info.SetImagePath(InfoUpgrade.defaultPath);
    }

    public override void OnLoad()
    {
        base.OnLoad();
        farmFieldsUnlocked = new List<FarmField>();
        GameState.instance.numbers.truckAmount.SetOnValueChanged(CheckUnlock);
    }

    public void CheckUnlock(long tiles)
    {
        Unlock();
        GameState.instance.numbers.truckAmount.ResetOnValueChanged(CheckUnlock);
    }


    public override void Apply()
    {
        return;
    }

    public override void Buy()
    {
        if(farmFieldsUnlocked.Count == 0)
        {
            unlock_pop_up.instance.ChangeText("KIMPOSSIBLE", "KNOSPACEAVAIBLEFORTRUCK");
            unlock_pop_up.instance.Animation();
        }
        else
        {
            base.Buy();
        }
    }
}
