using System;
using System.Collections.Generic;

[Serializable]
public class GameSettings
{
    //Volume settings

    public Dictionary<SoundCategory, double> soundVolumes;

    //Gameplay settings

    public bool buyLandOnHold;

    //setup default settings
    public void Init()
    {
        soundVolumes = new Dictionary<SoundCategory, double>();
        soundVolumes.Add(SoundCategory.PlayerMaster, 1.0);
        soundVolumes.Add(SoundCategory.PlayerMusic, 1.0);
        soundVolumes.Add(SoundCategory.PlayerSfx, 1.0);

        buyLandOnHold = false;
    }

    public void OnLoad()
    {
        
    }
}