using System;
using System.Collections.Generic;
using Godot;
using WJA23Godot.GameState;
using WJA23Godot.Upgrades;

public partial class FightManager : Node
{
    public static FightManager instance;
    
    [Export] public Node2D[] spawnPositions;
    [Export] public Timer spawnTimer;
    [Export] public PackedScene alienPrefab;

    [Export] public PackedScene winScreen;
    [Export] public PackedScene loseScreen;
    
    private bool[] spawnAvaible;
    private bool paused;

    public IdleAction<bool> OnSetPaused;
    public IdleAction<int> OnEnemyKill;

    public FightManager()
    {
        OnEnemyKill = new IdleAction<int>();
        OnSetPaused = new IdleAction<bool>();
        instance = this;
    }

    private int enemiesKilled = 0;

    private int enemiesSpawned = 0;

    public long enemyDamage;

    private Random rd;

    public long cachedHP;

    public override void _Ready()
    {
        base._Ready();
        
        spawnAvaible = new bool[spawnPositions.Length];
        for (int i = 0; i < spawnAvaible.Length; i++)
        {
            spawnAvaible[i] = true;
        }

        rd = new Random();

        if (spawnTimer == null)
        {
            GD.PrintErr("AAAA SPAWN TIMER NULL");
            return;
        }

        cachedHP = GameState.instance.numbers.cookedPotatoCount.GetValue();

        enemyDamage = (long)(Math.Pow(GameState.instance.numbers.fightWave.GetValue() * 15, 2) / 4f);  //Scaling v1
        
        GameState.instance.numbers.cookedPotatoCount.SetOnValueChanged(OnCookedPotatoChanged);

        OnEnemyKill += OnKillEnemy;

        OnSetPaused += OnPausedChanged;

        spawnTimer.Timeout += SpawnEnemy;
        StartNextSpawnTimer();
    }
    
    public override void _ExitTree()
    {
        base._ExitTree();

        instance = null;
    }

    public void OnCookedPotatoChanged(long value)
    {
        if (value <= 0)
        {
            GameState.instance.numbers.cookedPotatoCount.ResetOnValueChanged(OnCookedPotatoChanged);
            LoseFight();
        }
    }

    public void SetPaused(bool isPaused)
    {
        if (paused != isPaused)
        {
            paused = isPaused;
            OnSetPaused.Invoke(isPaused);
        }
    }

    public void OnPausedChanged(bool isPaused)
    {
        spawnTimer.Paused = isPaused;
    }

    public bool IsPaused() => paused;

    private void LoseFight()
    {
        SetPaused(true);
        
        if (loseScreen != null)
        {
            FightLoseScreen loseScreenInstance = loseScreen.Instantiate<FightLoseScreen>();
            GetTree().CurrentScene.AddChild(loseScreenInstance);
        }
    }

    private void WinFight()
    {
        List<IArtifact> lootedArtifacts = CalculateLoot();
        GameState.instance.numbers.fightWave.IncreaseValue(1);
        OnEnemyKill = null;
        
        SetPaused(true);

        if (winScreen != null)
        {
            FightWinScreen winScreenInstance = winScreen.Instantiate<FightWinScreen>();

            foreach (IArtifact artifact in lootedArtifacts)
            {
                winScreenInstance.AddDroppedArtifact(artifact);
            }
            
            GetTree().CurrentScene.AddChild(winScreenInstance);
        }
    }

    private List<IArtifact> CalculateLoot()
    {
        //+1 à fightwave se fait avant le calcul de loot
        List<IArtifact> lootedArtifacts = new();
        
        IArtifact newArtifact = GameState.instance.artifacts.GetRandomArtifact();
        newArtifact.Buy();
        lootedArtifacts.Add(newArtifact);

        return lootedArtifacts;
    }

    private float GetSpawnTime()
    {
        if (enemiesSpawned == 0)
        {
            return 1f;
        }
        
        return Math.Max(10f / (GameState.instance.numbers.fightWave.GetValue()+1), 1f); //scaling v1
    }

    private void StartNextSpawnTimer()
    {
        spawnTimer.Start(GetSpawnTime());
    }

    private long GetNbEnemiesToKill()
    {
        long wave = GameState.instance.numbers.fightWave.GetValue();
        return Math.Min(wave + 3, 10); //scaling v1
    }

    public void OnKillEnemy(int index)
    {
        enemiesKilled++;
        if (enemiesKilled >= GetNbEnemiesToKill())
        {
            //TODO Play fight end SFX
            WinFight();
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


