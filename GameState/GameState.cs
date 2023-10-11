using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using FileAccess = System.IO.FileAccess;
using Newtonsoft.Json;

[Serializable]
public partial class GameState
{
	public const bool SAVE_ENABLED = true;
	
	static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
	
	private static GameState _instance;
	public static GameState instance
	{
		get
		{
			if (_instance == null)
			{
				if (SAVE_ENABLED && LoadGame())
				{
					return _instance;
				}
				
				return NewGame();
			}
			return _instance;
		}
		set => _instance = value;
	}

	private const string filePath = "Save.sav";
	private const string settingsPath = "Settings.json";
	public int randomSeed = 0xBADF00D;
	public bool won;
	private Action OnWin;
	
	//Save file
	public IdleNumberContainer numbers;
	public IdleUpgradeContainer upgrades;
	public SavedFieldContainer savedFields;
	
	//Settings
	public static GameSettings settings;

	[NonSerialized] public static Dictionary<int, IdleModifier> allModifiers = new Dictionary<int, IdleModifier>();

	public void SetOnWin(Action onWin)
	{
		OnWin += onWin;
	}
	
	public void Init()
	{
		numbers = new IdleNumberContainer();
		numbers.Init();
		upgrades = new IdleUpgradeContainer();
		upgrades.Init();

		savedFields = new SavedFieldContainer();
		savedFields.Init();
		
		OnLoad();
	}

	protected void OnLoad()
	{
		numbers.OnLoad();
		upgrades.OnLoad();

		//debug with no save
		if (GameState.SAVE_ENABLED == false)
		{
			GameState.instance.numbers.potatoCount.SetValue(100000);
			//GameState.instance.numbers.cookedPotatoCount.SetValue(10000);
		}
	}
	
	public void SaveToFile()
	{
		string saveData = JsonConvert.SerializeObject(this, jsonSerializerSettings);
		
		using (StreamWriter outputFile = new StreamWriter(filePath, new FileStreamOptions{ Mode = FileMode.Create, Access = FileAccess.Write}))
		{
			outputFile.WriteLine(saveData);
			
			GD.Print("Saved!");
			unlock_pop_up.instance?.ChangeText("KSAVED", "");
			unlock_pop_up.instance?.Animation();
		}
	}

	public static bool LoadGame()
	{
		try
		{
			using (var sr = new StreamReader(filePath))
			{
				GameState newState;
				newState = JsonConvert.DeserializeObject<GameState>(sr.ReadToEnd(), jsonSerializerSettings);
				instance = newState;

				if (newState == null)
				{
					GD.PrintErr("AFTER LOADING STATE IS STILL NULL!");
					return false;
				}
				
				newState.OnLoad();

				return true;
			}
		}
		catch (IOException e)
		{
			//ERROR
			GD.PrintErr(e.Message);
		}

		return false;
	}

	public static GameState NewGame()
	{
		GameState newInstance = new GameState();
		
		Random random = new Random();
		newInstance.randomSeed = random.Next();

		instance = newInstance;
		
		newInstance.Init();
		return newInstance;
	}

	public void Win()
	{
		won = true;
		if (OnWin != null) OnWin();
	}

	public static void LoadSettings()
	{
		try
		{
			using (var sr = new StreamReader(settingsPath))
			{
				settings = JsonConvert.DeserializeObject<GameSettings>(sr.ReadToEnd());

				if (settings == null)
				{
					GD.PrintErr("AFTER LOADING SETTINGS ARE STILL NULL!");
					return;
				}
				
				settings.OnLoad();
			}
		}
		catch (IOException e)
		{
			//ERROR
			GD.PrintErr(e.Message);

			settings = new GameSettings();
			settings.Init();
			settings.OnLoad();
		}
	}

	public static void SaveSettings()
	{
		if (settings == null)
		{
			GD.PrintErr("Trying to save settings but they are null!");
		}
		
		string settingsData = JsonConvert.SerializeObject(settings);
		
		using (StreamWriter outputFile = new StreamWriter(settingsPath, new FileStreamOptions{ Mode = FileMode.Create, Access = FileAccess.Write}))
		{
			outputFile.WriteLine(settingsData);
		}
	}
}
