using Godot;
using System;
using System.Collections.Generic;

public partial class AddAutomaticTractorUpgrade : TieredUpgrade<MultiplierModifier>
{
	private List<FarmField> farmFieldsUnlocked = new List<FarmField>();
	public void AddField(FarmField toAdd)
	{
        farmFieldsUnlocked.Add(toAdd);
	}

    public override void OnBuy()
    {
        if(farmFieldsUnlocked.Count == 0)
        {
            //popup pas de field avaible ou mettre le tracteur
        }
        else
        {
            FarmField farmField = farmFieldsUnlocked[0];
            farmFieldsUnlocked.RemoveAt(0);
            Tracteur tractor = ResourceLoader.Load<PackedScene>("res://game_scenes/farm/tracteur/tracteur.tscn").Instantiate() as Tracteur;
            ObjectSpawner.Spawn(tractor, new Vector2(farmField.GlobalPosition.X, farmField.GlobalPosition.Y));
            tractor.automatic = true;
            tractor.topLeftBound = tractor.Position;
            tractor.bottomRightBound = new Vector2(tractor.Position.X + FarmFieldMaster.TILE_PER_FF * FarmFieldMaster.TILE_SIZE, tractor.Position.Y + FarmFieldMaster.TILE_PER_FF * FarmFieldMaster.TILE_SIZE);
            GameState.instance.numbers.truckAmount.IncreaseValue(1);
        }
        base.OnBuy();
    }

    public override void UpdateCost()
    {
        cost = 10000*tier;
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
}
