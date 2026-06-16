using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject HighScoreUI;

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinsText;

    private void Start()
    {
        UpdateCoinsUI();
        float highscore = PlayerPrefs.GetFloat("highscore", 0);
        Debug.Log(highscore);
        highScoreText.text = Mathf.FloorToInt(highscore).ToString();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    public void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            coinsText.text = MainManager.Instance.totalCoins.ToString();
        }
    }
    public void MenuScreen()
    {
        mainMenuUI.SetActive(true);
        HighScoreUI.SetActive(false);
    }

    public void HighscoreScreen()
    {
        HighScoreUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("GameSelectionScene");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void BuyStarMan()
    {
        if (MainManager.Instance.totalCoins >= 50)
        {
            MainManager.Instance.AddStars(1);
            MainManager.Instance.RemoveCoins(50);
            UpdateCoinsUI();
        }
    }
    public void BuyBigMario()
    {
        if (MainManager.Instance.totalCoins >= 30)
        {
            MainManager.Instance.AddBig(1);
            MainManager.Instance.RemoveCoins(30);
            UpdateCoinsUI();
        }
    }
}
