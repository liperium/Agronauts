using Godot;
using System;

public partial class FightWaveHandler : Node2D
{
    [Export] public Timer waveTimer;
    [Export] public BaseButton startWaveBtn;
    [Export] public RichTextLabel waveComingText;
    [Export] public CanvasLayer waveUICanvas;
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
        
        GameState.instance.numbers.currInvasionTimeLeft.SetOnValueChanged(UpdateTimer);
        
        UnlockFurnaceUpgrade unlockFurnaceUpgrade = GameState.instance.upgrades.unlockFurnaceUpgrade;
        if (unlockFurnaceUpgrade.IsUnlocked())
        {
            OnFurnaceUnlocked();
        }
        else
        {
            unlockFurnaceUpgrade.SetOnUnlock(OnFurnaceUnlocked);
        }
    }

    private void UpdateTimer(long timeleft)
    {
        waveTimer.Start(timeleft);
    }
    
    private void OnFurnaceUnlocked()
    {
        state = WaveState.InvasionComing;
        waveTimer.Start(GameState.instance.numbers.currInvasionTimeLeft.GetValue());
        
        //hide btn
        startWaveBtn.Modulate = new Color(1,1,1,0);
        startWaveBtn.Disabled = true;
        
        GameState.instance.upgrades.unlockFurnaceUpgrade.ResetOnUnlock(OnFurnaceUnlocked);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        if (waveTimer.TimeLeft == 0) return;
        
        if (waveTimer.TimeLeft <= 60 && state == WaveState.InvasionComing)
        {
            waveComingText.Text = Tr("KWAVECOMING") + " : " + Mathf.RoundToInt(waveTimer.TimeLeft) + " " +Tr("KSECONDS") + "!";

            if (waveUICanvas.Visible == false)
            {
                waveUICanvas.Visible = true;
            }
        }
        
        GameState.instance.numbers.currInvasionTimeLeft.SetValue((long)waveTimer.TimeLeft, false);
    }

    private void OnTimerEnd()
    {
        state = WaveState.InvasionStay;
        waveComingText.Text = Tr("KINVASIONARRIVED");
        waveTimer.Timeout -= OnTimerEnd;
        
        //show btn
        startWaveBtn.Modulate = new Color(1,1,1);
        startWaveBtn.Disabled = false;
    }
    

    private void StartWave()
    {
        if (GameState.instance.numbers.cookedPotatoCount.GetValue() == 0)
        {
            GamePopUp.instance.AddToQueue(new GamePopUpInfo("KIMPOSSIBLE","KNEEDCOOKEDPOTATOS"));
            return;
        }
        GameState.instance.numbers.currInvasionTimeLeft.SetValue(GameState.instance.numbers.invasionTime.GetValue());
        SceneTransition.GoToScene(fightScene);
    }

    public long GetWaveTime()
    {
        return GameState.instance.numbers.invasionTime.GetValue();
    }
    
}
