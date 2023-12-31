using Godot;
using System.Collections.Generic;

public partial class AddAutomaticTractorUpgrade : TieredUpgrade<MultiplierModifier>
{
	private List<FarmField> farmFieldsUnlocked;
	public void AddField(FarmField toAdd)
	{
        farmFieldsUnlocked.Add(toAdd);
	}

    public void ResetFarmFieldList()
    {
        farmFieldsUnlocked.Clear();
    }

    public override void OnBuy()
    {
        SpawnTractor();
        GameState.instance.numbers.truckAmount.IncreaseValue(1);

        base.OnBuy();
    }

    public void SpawnTractor()
    {
        FarmField farmField = farmFieldsUnlocked[0];
        farmFieldsUnlocked.RemoveAt(0);
        Tracteur tractor = ResourceLoader.Load<PackedScene>("res://game_scenes/farm/tracteur/tracteur.tscn").Instantiate() as Tracteur;

        if (tractor == null)
        {
            GD.PrintErr("TRACTOR IS NULL WHEN LOADING!!?? WTF");
            return;
        }
        
        ObjectSpawner.Spawn(tractor, new Vector2(farmField.GlobalPosition.X, farmField.GlobalPosition.Y));
        tractor.automatic = true;
        tractor.topLeftBound = tractor.Position;
        tractor.bottomRightBound = new Vector2(tractor.Position.X + FarmFieldMaster.TILE_PER_FF * FarmFieldMaster.TILE_SIZE, tractor.Position.Y + FarmFieldMaster.TILE_PER_FF * FarmFieldMaster.TILE_SIZE);
    }

    public override void UpdateCost()
    {
        cost = 10000*(tier+1);
    }


    public override void InitInfo()
    {
        base.InitInfo();
        info.SetName("KADDAUTOTRACTORUPGRADE");
        info.SetDescription("KADDAUTOTRACTORUPGRADEDESC");
        info.SetImagePath("res://Upgrades/UpgradeImages/ai.png");
    }

    public override void OnLoad()
    {
        base.OnLoad();
        farmFieldsUnlocked = new List<FarmField>();

        if (IsUnlocked() == false)
        {
            GameState.instance.numbers.truckAmount.SetOnValueChanged(CheckUnlock);
        }
    }

    public void SpawnAllTractors()
    {
        //spawn tractors by tier
        for (int i = 0; i < tier; i++)
        {
            SpawnTractor();
        }
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
            GamePopUp.instance.AddToQueue(new GamePopUpInfo("KIMPOSSIBLE", "KNOSPACEAVAIBLEFORTRUCK"));
        }
        else
        {
            base.Buy();
        }
    }

    public override string GetEffectText()
    {
        return "("+tier+")";
    }
    public override UIManager.UpgradeTab GetUpgradeTab()
    {
        return UIManager.UpgradeTab.Farm;
    }
}
