using System;
using System.Collections.Generic;
using Godot;

public partial class FightManager : Node
{
    [Export] public Node2D[] spawnPositions;
    [Export] public Timer spawnTimer;

    [Export] public PackedScene alienPrefab;

    public static Action OnEnemyKill;

    private int enemiesKilled = 0;

    private Random rd;

    public override void _Ready()
    {
        base._Ready();

        rd = new Random();

        if (spawnTimer == null)
        {
            GD.PrintErr("AAAA SPAWN TIMER NULL");
            return;
        }

        OnEnemyKill += OnKillEnemy;

        spawnTimer.Timeout += SpawnEnemy;
        StartNextSpawnTimer();

        //TODO fight layer for music
    }
    
    private void EndFight()
    {
        OnEnemyKill = null;
        
        //TODO load back to farm scene
        GD.Print("FIGHT END!");
        
    }

    private float GetSpawnTime()
    {
        return 10f / GameState.instance.numbers.fightWave.GetValue();
    }

    private void StartNextSpawnTimer()
    {
        spawnTimer.Start(GetSpawnTime());
    }

    private long GetNbEnemiesToKill()
    {
        long wave = GameState.instance.numbers.fightWave.GetValue();
        return wave * 2; //TODO proper scaling
    }

    public void OnKillEnemy()
    {
        enemiesKilled++;
        if (enemiesKilled >= GetNbEnemiesToKill())
        {
            //TODO Play fight end SFX
            EndFight();
        }
    }

    private void SpawnEnemy()
    {
        if (alienPrefab == null)
        {
            GD.PrintErr("NO ALIEN PREFAB SET!!!!");
            return;
        }
        
        int spawnIndex = rd.Next() % spawnPositions.Length;

        Node2D spawnPoint = spawnPositions[spawnIndex];

        Alien alienInstance = alienPrefab.Instantiate() as Alien;
        if (alienInstance != null)
        {
            alienInstance.manager = this;
            ObjectSpawner.Spawn(alienInstance, spawnPoint.Position);
        }
        else
        {
            GD.PrintErr("NULL ALIEN SPAWN! WTF");
        }

        StartNextSpawnTimer();
    }
}
