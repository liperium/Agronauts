using Godot;

public partial class Cheats : Node2D
{
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		
		if (!OS.IsDebugBuild())
		{
			return;
		}
		
		if (Input.IsActionPressed("add_potatos"))
		{
			GameState.instance.numbers.potatoCount.IncreaseValue(1000000);
		}

		if (Input.IsActionPressed("add_cooked_potatos"))
		{
			GameState.instance.numbers.cookedPotatoCount.IncreaseValue(1000000);
		}
		
		if (Input.IsActionJustReleased("nuke_save"))
		{
			GameState.DeleteSave();
		}
		
		if (Input.IsActionJustReleased("give_artifact"))
		{
			GameState.instance.artifacts.GetRandomArtifact().Buy();
		}

		if (Input.IsActionJustReleased("spawn_invasion"))
		{
			GameState.instance.numbers.currInvasionTimeLeft.SetValue(3);
		}

		if (Input.IsActionJustReleased("pause_fight"))
		{
			if (FightManager.instance != null)
			{
				FightManager.instance.SetPaused(!FightManager.instance.IsPaused());
				GD.Print(FightManager.instance.IsPaused() ? "PAUSED" : "UNPAUSED");
			}
		}
	}
}
