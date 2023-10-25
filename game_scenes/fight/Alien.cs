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
    private bool isDead;
    
    private long HP;
    private FightManager manager;
    
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

        manager = FightManager.instance;
        manager.OnSetPaused += OnPausedChanged;

        GetNode<AnimationTree>("AnimationTree").AnimationFinished += OnAnimationFinished;
        UpdateText();
    }

    private void TimerOnTimeout()
    {
        if (manager.IsPaused())
        {
            return;
        }
        
        if (audioStreamPlayer != null)
        {
            audioStreamPlayer.Stream = shootSound;
            audioStreamPlayer.Play();
        }
        GetNode<AnimationTree>("AnimationTree").Set("parameters/conditions/shoot",true);
        GameState.instance.numbers.cookedPotatoCount.DecreaseValue(manager.enemyDamage);
        timer.Start();
    }

    private void OnAnimationFinished(StringName animname)
    {
        if (animname == "Shoot")
        {
            GetNode<AnimationTree>("AnimationTree").Set("parameters/conditions/shoot",false);
        }
    }

    private void OnPausedChanged(bool isPaused)
    {
        timer.Paused = manager.IsPaused();
    }

    public long GetHP()
    {
        long wave = GameState.instance.numbers.fightWave.GetValue();
        return (long)Math.Pow(wave * 15, 2); //scaling v1
    }

    public void TakeDamage(long dmg)
    {
        HP -= dmg;
        
        if (HP <= 0 && audioStreamPlayer.Stream != dieSound && isDead == false)
        {
            isDead = true;
            healthBar.SetHealth(0);
            manager.OnEnemyKill.Invoke(spawnIndex);

            AudioStreamPlayer2D newPlayer = new AudioStreamPlayer2D();
            newPlayer.Stream = dieSound;
            newPlayer.GlobalPosition = GlobalPosition;
            newPlayer.Bus = "SFX";
            newPlayer.Finished += () => newPlayer.QueueFree();
            GetTree().CurrentScene.AddChild(newPlayer);
            newPlayer.Play();

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
