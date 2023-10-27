using System;
using WJA23Godot.Numbers;

[Serializable]
public class IdleNumberContainer
{
    public IdleNumber potatoCount;
    public IdleNumber potatoYield;
    public IdleNumber potatoGrowSpeed;

    public IdleNumber cookedPotatoCount;
    public IdleNumber cookedPotatoYield;
    
    public IdleNumber potatoTemperature;

    public IdleNumber furnaceAutoBakeSpeed;
    public IdleNumber furnaceTotalAutoCookedPotato;
    public IdleNumber furnaceBatchCount;
    public IdleNumber furnaceSpeed;


    public IdleNumber numberOfTilesUnlocked;

    public IdleNumber truckAmount;
    public IdleNumber truckSpeed;
    public IdleNumber truckPotatosFarmed;

    public IdleNumber fightWave;
    public IdleNumberBaseValue invasionTime;
    public IdleNumber currInvasionTimeLeft;
    public IdleNumber critChance;
    public IdleNumber damage;

    public void OnLoad()
    {
        potatoCount.OnLoad();
        potatoYield.OnLoad();
        potatoGrowSpeed.OnLoad();
        
        cookedPotatoCount.OnLoad();
        cookedPotatoCount.SetImagePath("res://UI/sprites/PotatoeFumante.png");
        cookedPotatoYield.OnLoad();

        potatoTemperature.OnLoad();
        
        furnaceAutoBakeSpeed.OnLoad();
        furnaceTotalAutoCookedPotato.OnLoad();
        furnaceBatchCount.OnLoad();
        furnaceSpeed.OnLoad();
        
        numberOfTilesUnlocked.OnLoad();
        
        truckAmount.OnLoad();
        truckSpeed.OnLoad();
        truckPotatosFarmed.OnLoad();

        fightWave.OnLoad();
        critChance.OnLoad();
        invasionTime.OnLoad();
        currInvasionTimeLeft.OnLoad();
        damage.OnLoad();
        
        //win condition
        potatoCount.SetOnValueChanged(CheckWin);
        fightWave.SetOnValueChanged(CheckWin);
    }

    private void CheckWin(long value)
    {
        if (!GameState.instance.won && (potatoCount.GetValue() >= long.MaxValue - 1 || fightWave.GetValue() >= 16))
        {
            GameState.instance.Win();
        }
    }
    
    public void Init()
    {
        potatoCount = new IdleNumber();
        potatoYield = new IdleNumber();
        potatoYield.SetValue(1, false);
        potatoGrowSpeed = new IdleNumber();
        potatoGrowSpeed.SetValue(1, false);

        cookedPotatoCount = new IdleNumber();
        cookedPotatoYield = new IdleNumber();
        cookedPotatoYield.SetValue(1, false);

        potatoTemperature = new IdleNumber();
        potatoTemperature.SetValue(10, false);

        furnaceAutoBakeSpeed = new IdleNumber();
        furnaceAutoBakeSpeed.SetValue(100, false);
        furnaceTotalAutoCookedPotato = new IdleNumber();
        furnaceBatchCount = new IdleNumber();
        furnaceBatchCount.SetValue(1, false);
        furnaceSpeed = new IdleNumber();
        furnaceSpeed.SetValue(1, false);
        
        truckAmount = new IdleNumber();
        truckSpeed = new IdleNumber();
        truckSpeed.SetValue(20, false);
        truckPotatosFarmed = new IdleNumber();

        numberOfTilesUnlocked = new IdleNumber();

        fightWave = new IdleNumber();
        fightWave.SetValue(1, false);

        invasionTime = new IdleNumberBaseValue();
        invasionTime.SetBaseValue(300);
        
        currInvasionTimeLeft = new IdleNumber();
        currInvasionTimeLeft.SetValue(invasionTime.GetBaseValue());
        
        critChance = new IdleNumber();
        damage = new IdleDamageNumber();
    }

  
}