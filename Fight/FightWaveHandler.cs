using Godot;
using System;

public partial class FightWaveHandler : Node2D
{
    [Export] public Timer waveTimer;

    private IdleNumber fightWave;

    private bool unlockSubscribed;

    [Export] public PackedScene fightScene;

    public override void _Ready()
    {
        base._Ready();

        waveTimer.Timeout += OnTimerEnd;
        
        UnlockFurnaceUpgrade unlockFurnaceUpgrade = GameState.instance.upgrades.unlockFurnaceUpgrade;
        if (unlockFurnaceUpgrade.IsUnlocked())
        {
            OnFurnaceUnlocked();
        }
        else
        {
            unlockFurnaceUpgrade.SetOnUnlock(OnFurnaceUnlocked);
            unlockSubscribed = true;
        }
    }

    private void OnFurnaceUnlocked()
    {
        waveTimer.Start(GetWaveTime());
        
        if (unlockSubscribed)
        {
            GameState.instance.upgrades.unlockFurnaceUpgrade.SetOnUnlock(OnFurnaceUnlocked);
            unlockSubscribed = false;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (waveTimer.TimeLeft != 0 && waveTimer.TimeLeft <= 60)
        {
            //TODO time remaining UI
        }
    }

    private void OnTimerEnd()
    {
        waveTimer.Timeout -= OnTimerEnd;
        
        //TODO VAGUE ARRIVE POPUP
        
        waveTimer.Timeout += WaveStayEnd;
        waveTimer.Start(GetInvasionStayTime());
    }

    private void WaveStayEnd()
    {
        //TODO Wave s'en va
        waveTimer.Timeout -= WaveStayEnd;
        waveTimer.Timeout -= OnTimerEnd;
        waveTimer.Start(GetWaveTime());
    }

    private void StartWave()
    {
        GD.Print("START WAVE");
        GetTree().ChangeSceneToPacked(fightScene);
    }

    public float GetWaveTime()
    {
        return 15f;
    }

    public float GetInvasionStayTime()
    {
        return 120f;
    }
}
