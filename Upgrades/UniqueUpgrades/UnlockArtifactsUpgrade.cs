
public class UnlockArtifactsUpgrade : BuyableUpgrade<IdleModifier>
{
    public override void OnLoad()
    {
        if (IsUnlocked() == false) GameState.instance.numbers.fightWave.SetOnValueChanged(OnFightWaveChange);
        base.OnLoad();
    }

    private void OnFightWaveChange(long obj)
    {
        if (obj > 1)
        {
            Unlock();
            Buy();
            
            GameState.instance.numbers.fightWave.ResetOnValueChanged(OnFightWaveChange);
        }
    }
    
    public override void UpdateCost()
    {
        cost = 0;
    }
}