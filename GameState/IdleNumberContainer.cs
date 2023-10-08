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

    public IdleNumber furnaceBatchCount;
    public IdleNumber furnaceSpeed;

    public IdleNumber numberOfTilesUnlocked;

    public IdleNumber truckAmount;
    public IdleNumber truckSpeed;


    public void Init()
    {
        potatoCount = new IdleNumber();
        potatoYield = new IdleNumber();
        potatoYield.SetValue(1);
        potatoGrowSpeed = new IdleNumber();
        potatoGrowSpeed.SetValue(1);

        cookedPotatoCount = new IdleNumber();
        cookedPotatoYield = new IdleNumber();
        cookedPotatoYield.SetValue(1);

        potatoTemperature = new IdleNumber();

        furnaceBatchCount = new IdleNumber();
        furnaceBatchCount.SetValue(1);
        furnaceSpeed = new IdleNumber();
        furnaceSpeed.SetValue(1);

        truckAmount = new IdleNumber();
        truckSpeed = new IdleNumber();
        truckSpeed.SetValue(20);

        numberOfTilesUnlocked = new IdleNumber();
    }

    public void OnLoad()
    {
        
    }
}