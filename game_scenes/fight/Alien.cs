using Godot;
using System;

public partial class Alien : Area2D
{

    [Export] public Timer timer;
    private AudioStreamPlayer2D audioStreamPlayer;
    [Export] public AudioStreamWav shootSound;
    [Export] public AudioStreamWav spawnSound;
    [Export] public AudioStreamWav dieSound;
    
    private long HP;
    public FightManager manager;
    
    public override void _Ready()
    {
        base._Ready();
        HP = GetHP();
        
        timer.Timeout += TimerOnTimeout;
        audioStreamPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        audioStreamPlayer.Stream = spawnSound;
        audioStreamPlayer.Play();
    }

    private void TimerOnTimeout()
    {
        audioStreamPlayer.Stream = shootSound;
        audioStreamPlayer.Play();
        GetNode<AnimatedSprite2D>("AlienSprite").Play("shoot");
        //TODO SFX shoot
        GameState.instance.numbers.cookedPotatoCount.DecreaseValue(FightManager.enemyDamage);
    }

    public long GetHP()
    {
        long wave = GameState.instance.numbers.fightWave.GetValue();
        return wave * 100; //TODO proper scaling
    }

    public void TakeDamage(long dmg)
    {
        HP -= dmg;
        
        GD.Print($"ALIEN HP IS {HP}");

        if (HP <= 0 && audioStreamPlayer.Stream != dieSound)
        {
            Action onEnemyKill = FightManager.OnEnemyKill;
            if (onEnemyKill != null) onEnemyKill();
            audioStreamPlayer.Stream = dieSound;
            audioStreamPlayer.Play();
            audioStreamPlayer.Finished += () => QueueFree();
            Visible = false;
        }
    }
}
