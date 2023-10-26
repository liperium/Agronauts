using Godot;
using System;
using System.Collections.Generic;

public partial class UIManager : CanvasLayer
{
	private PackedScene upgradeTemplate;

	private static List<IBuyable> all_upgrades = new List<IBuyable>();

	private static bool showAcquired;

	private static IdleAction<bool> OnShowAcquired = new IdleAction<bool>();

	private static List<IBuyable> newUnlocksToFlash = new List<IBuyable>();

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
		if (upgrade.GetUpgradeTab() == UpgradeTab.None)
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

		if (tabButton != null)
		{
			newUpgradeHolderUi.SetTab(tabButton);
			
			if (newUnlocksToFlash.Contains(upgrade))
			{
				tabButton.FlashTab();
				newUpgradeHolderUi.FlashUpgrade();
				newUnlocksToFlash.Remove(upgrade);
			}
		}
		else
		{
			GD.PrintErr($"NO TAB FOUND FOR UPGRADE {upgrade.GetType()}");
		}

		if (addToNode != null)
		{
			addToNode.AddChild(newUpgradeHolderUi);
		}
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
	
	public static void SetOnShowAcquired(Action<bool> action)
	{
		if (action == null)
		{
			GD.PrintErr("ERROR ACTION IS NULL WTF");
		}
		else
		{
			OnShowAcquired += action;
		}
	}
	
	public static void ResetOnShowAcquired(Action<bool> action)
	{
		if (action == null)
		{
			GD.PrintErr("ERROR ACTION IS NULL WTF");
		}
		else
		{
			OnShowAcquired.RemoveManual(action);
		}
	}

	public static void ShowAcquiredUpgrades(bool show)
	{
		showAcquired = show;
		OnShowAcquired.Invoke(show);
	}

	public static bool AreAcquiredUpgradesShown()
	{
		return showAcquired;
	}

	public static void AddNewUnlocksToFlash(IBuyable upgrade)
	{
		newUnlocksToFlash.Add(upgrade);
	}
}
