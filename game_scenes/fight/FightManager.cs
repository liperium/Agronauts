using System;
using System.Collections.Generic;
using Godot;

public partial class FightManager : Node
{
    private PackedScene farmScene;
    [Export] public Node2D[] spawnPositions;
    [Export] public Timer spawnTimer;

    [Export] public PackedScene alienPrefab;

    public static Action OnEnemyKill;

    private int enemiesKilled = 0;

    public static long enemyDamage;

    private Random rd;

    public override void _Ready()
    {
        base._Ready();

        farmScene = ResourceLoader.Load<PackedScene>("res://game_scenes/farm/farm.tscn");

        rd = new Random();

        if (spawnTimer == null)
        {
            GD.PrintErr("AAAA SPAWN TIMER NULL");
            return;
        }

        enemyDamage = (long)(GameState.instance.numbers.cookedPotatoCount.GetValue() * 0.03f);
        
        GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(OnCookedPotatoChanged);

        OnEnemyKill += OnKillEnemy;

        spawnTimer.Timeout += SpawnEnemy;
        StartNextSpawnTimer();

        //TODO fight layer for music
    }

    public void OnCookedPotatoChanged(long value)
    {
        if (value == 0)
        {
            GameState.instance.numbers.cookedPotatoCount.ResetOnValueChanged(OnCookedPotatoChanged);
            LoseFight();
        }
    }

    private void LoseFight()
    {
        GD.Print("FIGHT LOSE!");
        EndFight();
    }

    private void EndFight()
    {
        OnEnemyKill = null;
        
        //TODO load back to farm scene
        GetTree().ChangeSceneToPacked(farmScene);
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
            GameState.instance.numbers.fightWave.IncreaseValue(1);
            GD.Print("FIGHT WIN!");
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

        if (spawnPositions.Length == 0)
        {
            GD.PrintErr("NO SPAWN POSITIONS!! PLEASE SET THEM IN DATA");
            return;
        }
        
        int spawnIndex = rd.Next() % spawnPositions.Length;

        Node2D spawnPoint = spawnPositions[spawnIndex];

        Alien alienInstance = alienPrefab.Instantiate() as Alien;
        if (alienInstance != null)
        {
            alienInstance.manager = this;
            spawnPoint.AddChild(alienInstance);
            //ObjectSpawner.Spawn(alienInstance, spawnPoint.Position);
        }
        else
        {
            GD.PrintErr("NULL ALIEN SPAWN! WTF");
        }

        StartNextSpawnTimer();
    }
}
