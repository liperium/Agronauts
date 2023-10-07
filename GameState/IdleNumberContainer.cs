using System;

[Serializable]
public class IdleNumberContainer
{
    public IdleNumber potatoCount;
    public IdleNumber potatoYield;

    public IdleNumber cookedPotatoCount;
    public IdleNumber cookedPotatoYield;
    
    public IdleNumber potatoTemp;

    public IdleNumber furnaceBatchCount;

    public IdleNumber numberOfTilesUnlocked;

    public void Init()
    {
        potatoCount = new IdleNumber();
        potatoYield = new IdleNumber();
        potatoYield.SetValue(1);

        cookedPotatoCount = new IdleNumber();
        cookedPotatoYield = new IdleNumber();
        cookedPotatoYield.SetValue(1);

        potatoTemp = new IdleNumber();

        furnaceBatchCount = new IdleNumber();
        furnaceBatchCount.SetValue(1);

        numberOfTilesUnlocked = new IdleNumber();
    }

    public void OnLoad()
    {
        
    }
}