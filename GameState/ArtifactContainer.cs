using System;
using System.Collections.Generic;
using Godot;
using WJA23Godot.Upgrades;

namespace WJA23Godot.GameState;

public class ArtifactContainer
{
    private List<IArtifact> artifacts;

    public  ArtifactContainer()
    {
        artifacts = new List<IArtifact>();
    }

    public void AddArtifact(IArtifact artifact)
    {
        artifacts.Add(artifact);
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
        if (!artifacts.Contains(artifact))
        {
            GD.PrintErr("ARTIFACT IS NOT IN LIST");
        }
        
        return ((float)artifact.GetWeight() / GetTotalWeight()) * 100;

    }
}