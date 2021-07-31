using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static int HighScore = 0;
    public static bool Music = true;
    public static int TimesPlayed = 0;
    public static int SpaceGold;
    public static bool newColors = false;
    public static int skin = 0;
    public static int smoke = 0;

    public static bool soundEffects = true;

    public GameObject musicB;
    public GameObject soundEffectB;
    public Color onColor;
    public Color offColor;

    private void Start()
    {
        LoadData();

        if (!Music)
        {
            musicB.GetComponent<Image>().color = offColor;
            AudioManager.instance.Stop("Music");
        }
        else
        {
            musicB.GetComponent<Image>().color = onColor;
            AudioManager.instance.Play("Music");
        }

        if (!soundEffects)
        {
            soundEffectB.GetComponent<Image>().color = offColor;
        }
        else
        {
            soundEffectB.GetComponent<Image>().color = onColor;
        }

    }
    public void Play()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene("GameScene");
    }

    public void Store()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene("StoreScene");
    }

    public void Quit()
    {
        AudioManager.instance.Play("Click");
        Application.Quit();
    }

    public void MusicB()
    {
        AudioManager.instance.Play("Click");
        if (Music)
        {
            AudioManager.instance.Stop("Music");
            Music = false;
            musicB.GetComponent<Image>().color = offColor;
        }
        else
        {
            AudioManager.instance.Play("Music");
            Music = true;
            musicB.GetComponent<Image>().color = onColor;
        }
    }

    public void SoundEffectB()
    {
        AudioManager.instance.Play("Click");
        if (soundEffects)
        {
            soundEffects = false;
            soundEffectB.GetComponent<Image>().color = offColor;
        }
        else
        {
            soundEffects = true;
            soundEffectB.GetComponent<Image>().color = onColor;
        }
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + "/player.scores";
        //File.Delete(path);
        if (File.Exists(path))
        {
            PlayerData data = SaveSystem.LoadPlayer();

            if (data != null)
            {
                HighScore = data.HighestSaveScore;
                TimesPlayed = data.TimesPlayed2;
                Music = data.MusicSound;

                SpaceGold = data.spaceGold;
                newColors = data.NewColors;
                skin = data.Skin;
                smoke = data.Smoke;
            }
            else
            {
                Debug.Log("No Saved Data");
            }
        }
    }
}
