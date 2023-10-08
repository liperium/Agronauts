using Godot;
using System;

public partial class FightWaveHandler : Node2D
{
    [Export] public Timer waveTimer;

    private IdleNumber fightWave;

    private bool unlockSubscribed;

    public override void _Ready()
    {
        base._Ready();

        waveTimer.Timeout += OnTimerEnd;
        
        FirstTractorUpgrade firstTractorUpgrade = GameState.instance.upgrades.firstTractorUpgrade;
        if (firstTractorUpgrade.acquired)
        {
            OnFirstTractorUnlocked();
        }
        else
        {
            firstTractorUpgrade.OnBuyUpgrade += OnFirstTractorUnlocked;
            unlockSubscribed = true;
        }
    }

    private void OnFirstTractorUnlocked()
    {
        waveTimer.Start(GetWaveTime());
        
        if (unlockSubscribed)
        {
            GameState.instance.upgrades.firstTractorUpgrade.OnBuyUpgrade -= OnFirstTractorUnlocked;
            unlockSubscribed = false;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (waveTimer.TimeLeft != 0 && waveTimer.TimeLeft <= 60)
        {
            //TODO WAVE WARNING SFX
        }
    }

    private void OnTimerEnd()
    {
        StartWave();
    }

    private void StartWave()
    {
        GD.Print("START WAVE");
    }

    public float GetWaveTime()
    {
        return 15;
    }
}
