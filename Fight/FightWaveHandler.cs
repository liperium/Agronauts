using Godot;
using System;

public partial class FightWaveHandler : Node2D
{
    [Export] public Timer waveTimer;

    private IdleNumber fightWave;

    private bool unlockSubscribed;

    private RichTextLabel waveComingText;

    private WaveState state;

    [Export] public PackedScene fightScene;

    enum WaveState
    {
        InvasionComing,
        InvasionStay,
    }

    public override void _Ready()
    {
        base._Ready();

        waveComingText = GetNode<RichTextLabel>("WaveUI/WaveComingText");
        waveComingText.Text = "";
        
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
        state = WaveState.InvasionComing;
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

        if (waveTimer.TimeLeft != 0 && waveTimer.TimeLeft <= 60 && state == WaveState.InvasionComing)
        {
            //TODO time remaining UI
            waveComingText.Text = Tr("KWAVECOMING") + " : " + Mathf.RoundToInt(waveTimer.TimeLeft) + " " +Tr("KSECONDS") + "!";
        }
    }

    private void OnTimerEnd()
    {
        state = WaveState.InvasionStay;
        waveComingText.Text = "";
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
