using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using FileAccess = System.IO.FileAccess;
using Newtonsoft.Json;
using WJA23Godot.GameState;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
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

	private const string saveFilePath = "Save.sav";
	private const string settingsPath = "Settings.json";
	private IdleAction OnWin;

	//Save file
	[JsonProperty]
	public int randomSeed = 0xBADF00D;
	
	[JsonProperty]
	public IdleNumberContainer numbers;
	[JsonProperty]
	public IdleUpgradeContainer upgrades;
	[JsonProperty]
	public SavedFieldContainer savedFields;

	[JsonProperty]
	public bool won;

	public static GameScene gameScene;
	
	//Artifacts
	public ArtifactContainer artifacts;
	
	public static Dictionary<int, IdleModifier> allModifiers = new Dictionary<int, IdleModifier>();
	
	//Settings
	public static GameSettings settings;

	public GameState()
	{
		OnWin = new IdleAction();
	}

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
		artifacts = new ArtifactContainer();
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
		
		using (StreamWriter outputFile = new StreamWriter(saveFilePath, new FileStreamOptions{ Mode = FileMode.Create, Access = FileAccess.Write}))
		{
			outputFile.WriteLine(saveData);
			
			GD.Print("Saved!");
			GamePopUp.instance?.AddToQueue(new GamePopUpInfo("KSAVED", ""));
		}
	}

	public static bool LoadGame()
	{
		try
		{
			using (var sr = new StreamReader(saveFilePath))
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

	public static void DeleteSave()
	{
		try
		{
			if (File.Exists(saveFilePath))
			{
				File.Delete(saveFilePath);
				
				GD.Print("Deleted save!");
			}
		}
		catch (Exception ex)
		{
			GD.PrintErr(ex);
		}
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
		OnWin.Invoke();
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
