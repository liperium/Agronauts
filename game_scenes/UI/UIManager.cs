using Godot;
using System;
using System.Collections.Generic;

public partial class UIManager : CanvasLayer
{
	private PackedScene upgradeTemplate;

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
		if (upgrade.GetUpgradeTab() == UpgradeTab.None || (upgrade.IsOneTimeBuy() && upgrade.IsAcquired()))
		{
			return;
		}
		UpgradeHolderUI newUpgradeHolderUi = upgradeTemplate.Instantiate() as UpgradeHolderUI;
		if (newUpgradeHolderUi == null)
		{
			GD.PrintErr($"Failed to instantiate upgrade holder UI! this upgrade will not be instantiated: {upgrade.GetInfo().GetName()}");
			return;
		}
		
		newUpgradeHolderUi.Init(upgrade);

		if (upgrade.IsOneTimeBuy())
		{
			upgrade.SetOnBuyUpgrade(newUpgradeHolderUi.FreeMe);
		}

		Node addToNode = null;
		Tab tabButton = null;
		switch (upgrade.GetUpgradeTab())
		{
			case UpgradeTab.Farm:
				addToNode = GetNode<VBoxContainer>("LeftMenu/TextureRect2/MarginContainer/Control/Control/Farm/FarmUpgrades");
				tabButton = GetNode<Tab>("LeftMenu/TextureRect2/MarginContainer/Control/MarginContainer/HBoxContainer/FarmTabButton");
				break;
			case UpgradeTab.Furnace:
				addToNode = GetNode<VBoxContainer>("LeftMenu/TextureRect2/MarginContainer/Control/Control/Furnace2/Furnace/MarginContainer/FurnaceUpgrades");
				tabButton = GetNode<Tab>("LeftMenu/TextureRect2/MarginContainer/Control/MarginContainer/HBoxContainer/FurnaceTabButton");
				break;
			case UpgradeTab.Artifact:
				addToNode = GetNode<VBoxContainer>("LeftMenu/TextureRect2/MarginContainer/Control/Control/Artifact/ArtifactUpgrades");
				tabButton = GetNode<Tab>("LeftMenu/TextureRect2/MarginContainer/Control/MarginContainer/HBoxContainer/ArtifactTabButton");
				newUpgradeHolderUi.NoButton();
				break;
		}
		addToNode.AddChild(newUpgradeHolderUi);
		upgrade.SetOnUnlock(tabButton.FlashTab);
	}

	public static void AddAllUpgrade(IBuyable upgrade)
	{
		if (!all_upgrades.Contains(upgrade))
		{
			all_upgrades.Add(upgrade);
		}
		else
		{
			GD.PrintErr("AddAllUpgrade added same upgrade");
		}
	}


// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
