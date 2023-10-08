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
        
        FirstTractorUpgrade firstTractorUpgrade = GameState.instance.upgrades.firstTractorUpgrade;
        if (firstTractorUpgrade.acquired)
        {
            OnFirstTractorUnlocked();
        }
        else
        {
            firstTractorUpgrade.SetOnBuyUpgrade(OnFirstTractorUnlocked);
            unlockSubscribed = true;
        }
    }

    private void OnFirstTractorUnlocked()
    {
        waveTimer.Start(GetWaveTime());
        
        if (unlockSubscribed)
        {
            GameState.instance.upgrades.firstTractorUpgrade.ResetOnBuyUpgrade(OnFirstTractorUnlocked);
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
        GetTree().ChangeSceneToPacked(fightScene);
    }

    public float GetWaveTime()
    {
        return 15;
    }
}
