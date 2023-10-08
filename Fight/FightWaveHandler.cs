using Godot;
using System;

public partial class FightWaveHandler : Node2D
{
    [Export] public Timer waveTimer;
    [Export] public PackedScene fightScene;
    private IdleNumber fightWave;
    private bool alerted = false;

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
            if(alerted == false)
            {
                unlock_pop_up.instance.ChangeText("KWARNING", "KWARNINGDESC");
                unlock_pop_up.instance.Animation();
                alerted = true;
            }

        }
    }

    private void OnTimerEnd()
    {
        StartWave();
        alerted = false;
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
