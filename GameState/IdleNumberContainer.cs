using System;

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
    public IdleNumber invasionTime;

    public void OnLoad()
    {
        potatoCount.OnLoad();
        potatoYield.OnLoad();
        potatoGrowSpeed.OnLoad();
        
        cookedPotatoCount.OnLoad();
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
        potatoCount.SetImagePath(InfoUpgrade.defaultImagePath);
        potatoYield = new IdleNumber();
        potatoYield.SetValue(1, false);
        potatoGrowSpeed = new IdleNumber();
        potatoGrowSpeed.SetValue(1, false);

        cookedPotatoCount = new IdleNumber();
        cookedPotatoCount.SetImagePath("res://UI/sprites/PotatoeFumante.png");
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

        invasionTime = new IdleNumber();
        invasionTime.SetValue(5, false);
    }

  
}