using Godot;
using System;
using System.IO;
using TinyJson;
using FileAccess = System.IO.FileAccess;

[Serializable]
public partial class GameState
{
	public const bool SAVE_ENABLED = false;
	
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

	public void Init()
	{
		numbers = new IdleNumberContainer();
		numbers.Init();
		upgrades = new IdleUpgradeContainer();
		upgrades.Init();
		
		OnLoad();
	}

	protected void OnLoad()
	{
		numbers.OnLoad();
		upgrades.OnLoad();
		
		if (GameState.SAVE_ENABLED == false) GameState.instance.numbers.potatoCount.SetValue(100000);
	}
	
	public void SaveToFile()
	{
		string saveData = this.ToJson();
		
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
				newState = sr.ReadToEnd().FromJson<GameState>();
				instance = newState;
				
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
