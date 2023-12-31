using Godot;
using System;
using System.Collections.Generic;

public partial class PotatoBullet : Area2D
{
    [Export] public Timer despawnTimer;

    private Vector2 ogScale;
    private Vector2 minScale;

    private bool delegateSet;

    private List<Alien> aliensInside = new List<Alien>();

    public override void _Ready()
    {
        base._Ready();
        if (despawnTimer != null)
        {
            despawnTimer.Timeout += Despawn;
            delegateSet = true;
        }

        FightManager.instance.OnSetPaused += OnPausedChanged;

        AreaEntered += OnEnter;
        AreaExited += OnExit;
    }

    private void OnPausedChanged(bool isPaused)
    {
        despawnTimer.Paused = isPaused;
    }

    private void OnEnter(Area2D area)
    {
        if (area is Alien alien)
        {
            aliensInside.Add(alien);
        }
    }
    
    private void OnExit(Area2D area)
    {
        if (area is Alien alien)
        {
            aliensInside.Remove(alien);
        }
    }
    
    private void Despawn()
    {
        //damage aliens
        long damage = ((IdleDamageNumber)GameState.instance.numbers.damage).GetValueWithCrit();
        foreach (Alien alien in aliensInside)
        {
            alien.TakeDamage(damage);
        }
        
        
        QueueFree();
    }

    public void SetScale(Vector2 newScale)
    {
        ogScale = newScale;
        minScale = 0.1f * newScale;
        Scale = newScale;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (FightManager.instance.IsPaused())
        {
            return;
        }
        
        if (Scale <= minScale)
        {
            if (delegateSet)
            {
                despawnTimer.Timeout -= Despawn;
                delegateSet = false;
            }
            Despawn();
        }
        else
        {
            Scale -= new Vector2((float)delta, (float)delta) * Mathf.Max(ogScale.X * 1.5f, 1f);
        }
    }

    /*public static long GetBulletDamage()
    {
        long nbPotats = GameState.instance.numbers.cookedPotatoCount.GetValue();
        long temperature = GameState.instance.numbers.potatoTemperature.GetValue();
        long damage = (long)Math.Max((float)Math.Sqrt(nbPotats + 1) * Math.Sqrt(temperature + 1), 1f);
        Random rd = new Random();
        if ((rd.Next() % 100) + 1 <= GameState.instance.numbers.critChance.GetValue())
        {
            damage *= 2;
        }
        return damage; 
    }*/
}
