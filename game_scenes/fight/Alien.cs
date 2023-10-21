using Godot;
using System;

public partial class Alien : Area2D
{

    [Export] public Timer timer;
    [Export] public HealthBar healthBar;
    private AudioStreamPlayer2D audioStreamPlayer;
    [Export] public AudioStreamWav shootSound;
    [Export] public AudioStreamWav spawnSound;
    [Export] public AudioStreamWav dieSound;
    public int spawnIndex = -1;
    
    private long HP;
    public FightManager manager;
    
    public override void _Ready()
    {
        base._Ready();
        HP = GetHP();
        healthBar.SetMaxHealth(HP);
        healthBar.SetHealth(HP);

        timer.Timeout += TimerOnTimeout;
        audioStreamPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

        audioStreamPlayer.Stream = spawnSound;
        audioStreamPlayer.Play();
        audioStreamPlayer.TreeExiting += () => audioStreamPlayer = null;

        GetNode<AnimationTree>("AnimationTree").AnimationFinished += OnAnimationFinished;
        UpdateText();
    }

    private void TimerOnTimeout()
    {
        if (audioStreamPlayer != null)
        {
            audioStreamPlayer.Stream = shootSound;
            audioStreamPlayer.Play();
        }
        GetNode<AnimationTree>("AnimationTree").Set("parameters/conditions/shoot",true);
        //TODO SFX shoot
        GameState.instance.numbers.cookedPotatoCount.DecreaseValue(FightManager.enemyDamage);
        timer.Start();
    }

    private void OnAnimationFinished(StringName animname)
    {
        if (animname == "Shoot")
        {
            GetNode<AnimationTree>("AnimationTree").Set("parameters/conditions/shoot",false);
        }
    }

    public long GetHP()
    {
        long wave = GameState.instance.numbers.fightWave.GetValue();
        return wave * 15; //TODO proper scaling
    }

    public void TakeDamage(long dmg)
    {
        HP -= dmg;
        
        GD.Print($"ALIEN HP IS {HP}");

        if (HP <= 0 && audioStreamPlayer.Stream != dieSound)
        {
            healthBar.SetHealth(0);
            Action<int> onEnemyKill = FightManager.OnEnemyKill;
            if (onEnemyKill != null) onEnemyKill(spawnIndex);
            /*if (audioStreamPlayer != null)
            {
                audioStreamPlayer.Stream = dieSound;
                audioStreamPlayer.Play();
                audioStreamPlayer.Finished += KillThis;
                audioStreamPlayer.TreeExiting += () =>
                {
                    if (audioStreamPlayer != null)
                    {
                        audioStreamPlayer.Finished -= KillThis;
                        audioStreamPlayer = null;
                    }
                };
            }*/ 
            //Hide();
            QueueFree();
        }
        else
        {
            healthBar.SetHealth(HP);
        }
        UpdateText();
    }

    private void UpdateText()
    {
        healthBar.text.Text = $"[center]{healthBar.CurrentHealth.FormattedNumber()}";
    }

    private void KillThis()
    {
        QueueFree();
    }
}
