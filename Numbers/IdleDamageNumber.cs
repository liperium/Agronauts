using System;
using WJA23Godot.Modifiers;

public class IdleDamageNumber : IdleNumber
{
    private AdditiveModifier modifier;
    
    public IdleDamageNumber()
    {
        modifier = new AdditiveModifier();
        modifier.SetOwner(this);
    }

    public override void OnLoad()
    {
        base.OnLoad();
        modifier.AddModifier();
        GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(UpdateModifier);
        GameState.instance.numbers.potatoTemperature.SetOnValueChanged(UpdateModifier);
        UpdateModifier();
    }

    private void UpdateModifier(long value = 0)
    {
        long nbPotats = GameState.instance.numbers.cookedPotatoCount.GetValue();
        long temperature = GameState.instance.numbers.potatoTemperature.GetValue();
        modifier.addition = (long)Math.Max((float)Math.Sqrt(nbPotats + 1) * Math.Sqrt(temperature + 1), 1f);
        UpdateValue();
    }
    

    public long GetValueWithCrit()
    {
        Random rd = new Random();
        if ((rd.Next() % 100) + 1 <= GameState.instance.numbers.critChance.GetValue())
        {
            return base.GetValue() * 2;
        }
        return base.GetValue();
    }
}