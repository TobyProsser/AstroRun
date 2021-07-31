using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StoreScript : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    public int SolarSystemCost = 20;

    private SaveDataScript SaveData;

    public TextMeshProUGUI SolarCostText;

    public GameObject PlanetsPanel;
    public GameObject SmokePanel;
    public GameObject SkinPanel;

    private void Awake()
    {
        goldText.text = MenuController.SpaceGold.ToString();
        SolarCostText.text = SolarSystemCost.ToString();

        BackButton();
    }

    public void BackButton()
    {
        PlanetsPanel.SetActive(false);
        SmokePanel.SetActive(false);
        SkinPanel.SetActive(false);

        AudioManager.instance.Play("Click");
    }

    public void PlanetsButton()
    {
        PlanetsPanel.SetActive(true);

        AudioManager.instance.Play("Click");
    }

    public void SmokeButton()
    {
        SmokePanel.SetActive(true);

        AudioManager.instance.Play("Click");
    }

    public void SkinButton()
    {
        SkinPanel.SetActive(true);

        AudioManager.instance.Play("Click");
    }

    public void SolarSystemB()
    {
        if (MenuController.SpaceGold >= SolarSystemCost)
        {
            AudioManager.instance.Play("Click");
            MenuController.newColors = true;
            MenuController.SpaceGold -= SolarSystemCost;
            print("Buy");
        }
        goldText.text = MenuController.SpaceGold.ToString();

        BackButton();
    }

    public void OriginalPlanetB()
    {
        AudioManager.instance.Play("Click");
        MenuController.newColors = false;
        goldText.text = MenuController.SpaceGold.ToString();

        BackButton();
    }

    public void OGSkinB()
    {
        AudioManager.instance.Play("Click");
        MenuController.skin = 0;
        goldText.text = MenuController.SpaceGold.ToString();

        BackButton();
    }

    public void FishSkinB()
    {
        if (MenuController.SpaceGold >= 100)
        {
            AudioManager.instance.Play("Click");
            MenuController.skin = 1;
            MenuController.SpaceGold -= 100;
            goldText.text = MenuController.SpaceGold.ToString();

            BackButton();
        }
    }

    public void Smoke(int type)
    {
        print(type);
        if (type == 0)
        {
            MenuController.smoke = type;
            AudioManager.instance.Play("Click");
            BackButton();
        }
        else if (type == 1 && MenuController.SpaceGold >= 15)
        {
            MenuController.smoke = type;
            AudioManager.instance.Play("Click");
            MenuController.SpaceGold -= 15;
            BackButton();
        }
        else if (type == 2 && MenuController.SpaceGold >= 20)
        {
            MenuController.smoke = type;
            AudioManager.instance.Play("Click");
            MenuController.SpaceGold -= 20;
            BackButton();
        }
        else if (type == 3 && MenuController.SpaceGold >= 25)
        {
            MenuController.smoke = type;
            AudioManager.instance.Play("Click");
            MenuController.SpaceGold -= 25;
            BackButton();
        }
        else if (type == 4 && MenuController.SpaceGold >= 30)
        {
            MenuController.smoke = type;
            AudioManager.instance.Play("Click");
            MenuController.SpaceGold -= 30;
            BackButton();
        }
        else if (type == 5 && MenuController.SpaceGold >= 35)
        {
            MenuController.smoke = type;
            AudioManager.instance.Play("Click");
            MenuController.SpaceGold -= 35;
            BackButton();
        }
        else if (type == 6 && MenuController.SpaceGold >= 40)
        {
            MenuController.smoke = type;
            AudioManager.instance.Play("Click");
            MenuController.SpaceGold -= 40;
            BackButton();
        }
        else if (type == 7 && MenuController.SpaceGold >= 45)
        {
            MenuController.smoke = type;
            AudioManager.instance.Play("Click");
            MenuController.SpaceGold -= 45;
            BackButton();

        }
        else if (type == 8 && MenuController.SpaceGold >= 100)
        {
            MenuController.smoke = type;
            AudioManager.instance.Play("Click");
            MenuController.SpaceGold -= 100;
            BackButton();

        }
        goldText.text = MenuController.SpaceGold.ToString();
    }

    public void MainMenuB()
    {
        Save();
        AudioManager.instance.Play("Click");
    }

    private void Save()
    {
        SaveData = GameObject.Find("SaveObject").GetComponent<SaveDataScript>();

        SaveData.HighestScore1 = MenuController.HighScore;
        SaveData.TimesPlayed1 = MenuController.TimesPlayed;
        SaveData.MusicSound1 = MenuController.Music;

        SaveData.spaceGold1 = MenuController.SpaceGold;
        SaveData.NewColors1 = MenuController.newColors;
        SaveData.Skin1 = MenuController.skin;
        SaveData.Smoke1 = MenuController.smoke;

        SaveSystem.SavePlayer(SaveData);

        SceneManager.LoadScene("MainMenu");                                 //Go back to menu after saving, not before
    }

    //00BCC5, FA4C00, FF1010, 5500C5, FF00EB, 08F11F, 0023FF, FF9E00
}
