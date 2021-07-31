using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int HighestSaveScore;
    public int TimesPlayed2;
    public bool MusicSound;

    public int spaceGold;
    public bool NewColors;
    public int Skin;
    public int Smoke;

    public PlayerData(SaveDataScript player)
    {
        HighestSaveScore = player.HighestScore1;
        TimesPlayed2 = player.TimesPlayed1;
        MusicSound = player.MusicSound1;

        spaceGold = player.spaceGold1;
        NewColors = player.NewColors1;
        Skin = player.Skin1;
        Smoke = player.Smoke1;
    }
}
