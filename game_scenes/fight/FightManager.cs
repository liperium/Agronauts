using System;
using System.Collections.Generic;
using Godot;

public partial class FightManager : Node
{
    private PackedScene farmScene;
    [Export] public Node2D[] spawnPositions;
    private bool[] spawnAvaible;
    [Export] public Timer spawnTimer;

    [Export] public PackedScene alienPrefab;

    public static Action<int> OnEnemyKill;

    private int enemiesKilled = 0;

    private int enemiesSpawned = 0;

    public static long enemyDamage;

    private Random rd;

    public override void _Ready()
    {
        base._Ready();

        spawnAvaible = new bool[spawnPositions.Length];
        for (int i = 0; i < spawnAvaible.Length; i++)
        {
            spawnAvaible[i] = true;
        }

        farmScene = ResourceLoader.Load<PackedScene>("res://game_scenes/farm/farm.tscn");

        rd = new Random();

        if (spawnTimer == null)
        {
            GD.PrintErr("AAAA SPAWN TIMER NULL");
            return;
        }

        enemyDamage = 1; //TODO FIGURE SOMETHING OUT
        
        GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(OnCookedPotatoChanged);

        OnEnemyKill += OnKillEnemy;

        spawnTimer.Timeout += SpawnEnemy;
        StartNextSpawnTimer();

        //TODO fight layer for music
    }

    public void OnCookedPotatoChanged(long value)
    {
        if (value <= 0)
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
        CalculateLoot();
        OnEnemyKill = null;
        
        //TODO load back to farm scene
        GetTree().ChangeSceneToPacked(farmScene);
        GD.Print("FIGHT END!");
        
    }

    private void CalculateLoot()
    {
        //+1 à fightwave ce fait avant le calcul de loot
        if (GameState.instance.numbers.fightWave.GetValue() == 2)
        {
            GameState.instance.artifacts.GetRandomArtifact().Buy();
            return;
        }
        
        if (rd.Next() % 3 < 2)
        {
            GameState.instance.artifacts.GetRandomArtifact().Buy();
        }
    }

    private float GetSpawnTime()
    {
        if (enemiesSpawned == 0)
        {
            return 1f;
        }
        
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

    public void OnKillEnemy(int index)
    {
        enemiesKilled++;
        if (enemiesKilled >= GetNbEnemiesToKill())
        {
            //TODO Play fight end SFX
            GameState.instance.numbers.fightWave.IncreaseValue(1);
            GD.Print("FIGHT WIN!");
            EndFight();
        }
        else
        {
            spawnAvaible[index] = true;
        }
        
    }

    private int GetRandomPosition()
    {
        bool full = true;
        List<int> avaibleSpots = new List<int>();
        for(int i = 0; i < spawnAvaible.Length; i++)
        {
            bool avaible = spawnAvaible[i];
            if (avaible)
            {
                full = false;
                avaibleSpots.Add(i);
            }
        }

        if (full) return -1;

        int index =  rd.Next() % avaibleSpots.Count;
        return avaibleSpots[index];
    }

    private void SpawnEnemy()
    {
        if (enemiesSpawned >= GetNbEnemiesToKill()) return;
        
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
        
        int spawnIndex = GetRandomPosition();

        if (spawnIndex != -1)
        {
            Node2D spawnPoint = spawnPositions[spawnIndex];

            Alien alienInstance = alienPrefab.Instantiate() as Alien;
            if (alienInstance != null)
            {
                alienInstance.manager = this;
                alienInstance.spawnIndex = spawnIndex;
                spawnAvaible[spawnIndex] = false;
                spawnPoint.AddChild(alienInstance);
                enemiesSpawned++;
                //ObjectSpawner.Spawn(alienInstance, spawnPoint.Position);
            }
            else
            {
                GD.PrintErr("NULL ALIEN SPAWN! WTF");
            } 
        }
        
        StartNextSpawnTimer();
    }
}


