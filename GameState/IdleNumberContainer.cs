using System;

[Serializable]
public class IdleNumberContainer
{
    public IdleNumber potatoCount = new();
    public IdleNumber potatoYield = new();

    public IdleNumber cookedPotatoCount = new();
    public IdleNumber cookedPotatoYield = new();
    
    public IdleNumber potatoTemp = new();
}