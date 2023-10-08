using Godot;
using System;

public partial class Alien : Area2D
{

    [Export] public Timer timer;
    
    private long HP;
    public FightManager manager;
    
    public override void _Ready()
    {
        base._Ready();
        HP = GetHP();
        
        timer.Timeout += TimerOnTimeout;
    }

    private void TimerOnTimeout()
    {
        //TODO SFX shoot
        GameState.instance.numbers.cookedPotatoCount.DecreaseValue(FightManager.enemyDamage);
        
        QueueFree();
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

        if (HP <= 0)
        {
            Action onEnemyKill = FightManager.OnEnemyKill;
            if (onEnemyKill != null) onEnemyKill();
            QueueFree();
        }
    }
}
