using Godot;
using System;

public partial class FightWaveHandler : Node2D
{
    [Export] public Timer waveTimer;
    [Export] public BaseButton startWaveBtn;
    [Export] public RichTextLabel waveComingText;
    [Export] public CanvasLayer waveUICanvas;

    private bool unlockSubscribed;

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

        waveComingText.Text = "";
        waveUICanvas.Visible = false;
        
        waveTimer.Timeout += OnTimerEnd;
        startWaveBtn.Pressed += StartWave;
        
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
        
        //hide btn
        startWaveBtn.Modulate = new Color(1,1,1,0);
        startWaveBtn.Disabled = true;
        
        if (unlockSubscribed)
        {
            GameState.instance.upgrades.unlockFurnaceUpgrade.ResetOnUnlock(OnFurnaceUnlocked);
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

            if (waveUICanvas.Visible == false)
            {
                waveUICanvas.Visible = true;
            }
        }
    }

    private void OnTimerEnd()
    {
        state = WaveState.InvasionStay;
        waveComingText.Text = Tr("KINVASIONARRIVED");
        waveTimer.Timeout -= OnTimerEnd;
        
        //show btn
        startWaveBtn.Modulate = new Color(1,1,1);
        startWaveBtn.Disabled = false;
        
        waveTimer.Timeout += WaveStayEnd;
        waveTimer.Start(GetInvasionStayTime());
    }

    private void WaveStayEnd()
    {
        //TODO Wave s'en va
        waveTimer.Timeout -= WaveStayEnd;
        waveTimer.Start(GetWaveTime());
    }

    private void StartWave()
    {
        GD.Print("START WAVE");
        SceneTransition.GoToScene(fightScene);
    }

    public long GetWaveTime()
    {
        return GameState.instance.numbers.invasionTime.GetValue();
    }

    public float GetInvasionStayTime()
    {
        return 120f;
    }
}
