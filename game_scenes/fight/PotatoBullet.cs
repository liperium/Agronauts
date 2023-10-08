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

        AreaEntered += OnEnter;
        AreaExited += OnExit;
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
        long damage = GetBulletDamage();
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
            Scale -= new Vector2((float)delta, (float)delta);
        }
    }

    public long GetBulletDamage()
    {
        return GameState.instance.numbers.cookedPotatoCount.GetValue() + 10; //TODO POLISH
    }
}