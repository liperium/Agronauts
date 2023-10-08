using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using TinyJson;
using FileAccess = System.IO.FileAccess;
using Newtonsoft.Json;

[Serializable]
public partial class GameState
{
	public const bool SAVE_ENABLED = true;
	
	static JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
	
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
	public int randomSeed = 0xBADF00D;

	public IdleNumberContainer numbers;
	public IdleUpgradeContainer upgrades;
	public SavedFieldContainer savedFields;

	[NonSerialized] public static Dictionary<int, IdleModifier> allModifiers = new Dictionary<int, IdleModifier>();

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
			GameState.instance.numbers.cookedPotatoCount.SetValue(10000);
		}

	}
	
	public void SaveToFile()
	{
		string saveData = JsonConvert.SerializeObject(this, settings);
		
		// Write the string array to a new file named "WriteLines.txt".
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
			// Open the text file using a stream reader.
			using (var sr = new StreamReader(filePath))
			{
				// Read the stream as a string, and write the string to the console.
				GameState newState;
				newState = JsonConvert.DeserializeObject<GameState>(sr.ReadToEnd(), settings);
				//newState = sr.ReadToEnd().FromJson<GameState>();
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
			GD.Print(e.Message);
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
}
