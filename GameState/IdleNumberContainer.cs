using System;

[Serializable]
public class IdleNumberContainer
{
    public IdleNumber potatoCount;
    public IdleNumber potatoYield;

    public IdleNumber cookedPotatoCount;
    public IdleNumber cookedPotatoYield;
    
    public IdleNumber potatoTemp;

    public void Init()
    {
        potatoCount = new IdleNumber();
        potatoYield = new IdleNumber();

        cookedPotatoCount = new IdleNumber();
        cookedPotatoYield = new IdleNumber();

        potatoTemp = new IdleNumber();
    }

    public void OnLoad()
    {
        
    }
}