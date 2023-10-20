using System;
using System.Collections.Generic;
using Godot;
using WJA23Godot.Upgrades;

namespace WJA23Godot.GameState;

public class ArtifactContainer
{
    private List<IArtifact> artifacts = new();

    public void AddArtifact(IArtifact artifact)
    {
        artifacts.Add(artifact);
        if (artifact.IsMaxed() == false)
        {
            //TODO essayer de figure out une way de clean up
            artifact.SetOnMaxedUpgrade(UpdateArtifactsInfo);
        }
    }

    private void UpdateArtifactsInfo()
    {
        foreach (IArtifact a in artifacts)
        {
            a.InitInfo();
        }
    }

    public void RemoveArtifact(IArtifact artifact)
    {
        artifacts.Remove(artifact);
    }

    public List<IArtifact> GetArtifactList()
    {
        return artifacts;
    }

    private int GetTotalWeight()
    {
        int weight = 0;
        
        foreach (IArtifact a in artifacts)
        {
            if (a.IsMaxed()) continue;
            weight += a.GetWeight();
        }

        return weight;
    }

    public IArtifact GetRandomArtifact()
    {
        Random rand = new Random();
        int roll = rand.Next(GetTotalWeight());
        IArtifact artifactRolled = null;
        
        int count = -1; //Starts at -1 so an artifact with a weight of 0 can't be selected. trust
        foreach (IArtifact a in artifacts)
        {
            if (a.IsMaxed()) continue;
            count += a.GetWeight();
            if (count >= roll)
            {
                artifactRolled = a;
                break;
            }
        }

        if (artifactRolled == null)
        {
            GD.PrintErr("ARTIFACT ROLLED IS NULL");
        }
        return artifactRolled;
    }

    public float GetArtifactDropChancePercentage(IArtifact artifact)
    {
        if (artifact.IsMaxed()) return 0;
        
        if (!artifacts.Contains(artifact))
        {
            GD.PrintErr("ARTIFACT IS NOT IN LIST");
        }
        
        return ((float)artifact.GetWeight() / GetTotalWeight()) * 100;

    }
}