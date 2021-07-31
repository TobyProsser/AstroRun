using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BetweenGameScript : MonoBehaviour
{
    public static int score;

    private SaveDataScript SaveData;

    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI highScoreText;

    public static bool canRespawn = true;
    public GameObject continueButton;

    public TextMeshProUGUI goldText;

    void Start()
    {
        if (score > MenuController.HighScore)
        {
            MenuController.HighScore = score;
            CloudOnceServices.instance.SubmitScoreToLeaderboard(score);
        }
        lastScoreText.text = "Last Score: " + score.ToString();
        highScoreText.text = "Top Score: " + MenuController.HighScore.ToString();

        if (score >= 5)
        {
            MenuController.TimesPlayed++;
        }

        if (MenuController.TimesPlayed >= 8)
        {
            AdController.AdInstance.ShowAd("video");
            MenuController.TimesPlayed = 0;
        }

        Save();

        if (canRespawn) continueButton.SetActive(true);
        else continueButton.SetActive(false);

        goldText.text = MenuController.SpaceGold.ToString();
    }

    public void PlayAgain()
    {
        AudioManager.instance.Play("Click");
        canRespawn = true;
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        AudioManager.instance.Play("Click");
        AdController.AdInstance.ShowAd("rewardedVideo");
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
    }
}
