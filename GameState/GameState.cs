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
				if (LoadGame())
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

	public IdleNumberContainer numbers = new();
	public IdleUpgradeContainer upgrades = new();
	
	public void SaveToFile()
	{
		string saveData = this.ToJson();
		
		// Write the string array to a new file named "WriteLines.txt".
		using (StreamWriter outputFile = new StreamWriter(filePath, new FileStreamOptions{ Mode = FileMode.Create, Access = FileAccess.Write}))
		{
			outputFile.WriteLine(saveData);
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
		
		return newInstance;
	}
}
