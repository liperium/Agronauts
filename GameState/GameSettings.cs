﻿using System;
using System.Collections.Generic;
using Godot;

[Serializable]
public class GameSettings
{
    //Volume settings

    public Dictionary<SoundCategory, double> soundVolumes;

    //Gameplay settings

    public bool buyLandOnHold;
    public bool saveOnQuit;

    // Window settings
    public long windowMode;
    public Pos2D lastWindowedSize;
    public Pos2D lastWindowedPos;

    //setup default settings
    public void Init()
    {
        soundVolumes = new Dictionary<SoundCategory, double>();
        soundVolumes.Add(SoundCategory.PlayerMaster, 1.0);
        soundVolumes.Add(SoundCategory.PlayerMusic, 1.0);
        soundVolumes.Add(SoundCategory.PlayerSfx, 1.0);
        soundVolumes.Add(SoundCategory.PlayerTractor, 1.0);

        buyLandOnHold = false;
        windowMode = (int)DisplayServer.WindowMode.Maximized;
    }

    public void OnLoad()
    {
        
    }
}