using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataScript : MonoBehaviour
{
    public static SaveDataScript instance1;

    public int HighestScore1;
    public int TimesPlayed1;
    public bool MusicSound1;

    public int spaceGold1;
    public bool NewColors1;
    public int Skin1;
    public int Smoke1;
    private void Awake()
    {
        if (instance1 == null)
        {
            instance1 = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
