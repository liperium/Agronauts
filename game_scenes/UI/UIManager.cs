using Godot;
using System;
using System.Collections.Generic;

public partial class UIManager : CanvasLayer
{
	private PackedScene upgradeTemplate;

	public static UIManager instance;

	private static List<IBuyable> all_upgrades = new List<IBuyable>();

	public enum UpgradeTab
	{
		Farm,
		Furnace,
		Artifact,
		None,
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		upgradeTemplate = ResourceLoader.Load<PackedScene>("res://game_scenes/UI/UpgradeTemplate.tscn");

		foreach (var up in all_upgrades)
		{
			InstantiateUpgradeUI(up);
		}
	}

	public void InstantiateUpgradeUI(IBuyable upgrade)
	{
		if (upgrade.GetUpgradeTab() == UpgradeTab.None || (upgrade.IsOneTimeBuy()&&upgrade.IsAcquired()))
		{
			return;
		}
		UpgradeHolderUI newUpgradeHolderUi = upgradeTemplate.Instantiate() as UpgradeHolderUI;
		newUpgradeHolderUi.Init(upgrade);

		if (upgrade.IsOneTimeBuy())
		{
			upgrade.SetOnBuyUpgrade(newUpgradeHolderUi.FreeMe);
		}

		Node addToNode = null;
		switch (upgrade.GetUpgradeTab())
		{
			case UpgradeTab.Farm:
				addToNode = GetNode<VBoxContainer>("LeftMenu/TextureRect2/MarginContainer/Control/Control/Farm/FarmUpgrades");
				break;
			case UpgradeTab.Furnace:
				addToNode = GetNode<VBoxContainer>("LeftMenu/TextureRect2/MarginContainer/Control/Control/Furnace/VBoxContainer/MarginContainer/FurnaceUpgrades");
				break;
			case UpgradeTab.Artifact:
				addToNode = GetNode<VBoxContainer>("LeftMenu/TextureRect2/MarginContainer/Control/Control/Artifact/ArtifactUpgrades");
				newUpgradeHolderUi.NoButton();
				break;
		}
		addToNode.AddChild(newUpgradeHolderUi);

	}

	public static void AddAllUpgrade(IBuyable upgrade)
	{
		if (!all_upgrades.Contains(upgrade))
		{
			all_upgrades.Add(upgrade);
		}

		else
		{
			GD.Print("YOOOOO SHOULDNT BE HERE");
		}

	}


// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
