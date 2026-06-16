using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class M_UiManager : MonoBehaviour
{
    public static M_UiManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameSpeed = initialGameSpeed;
        Application.targetFrameRate = 120;

        UpdateHighscoreText();
    }

    public void UpdateScore(float newScore)
    {
        scoreText.text = Mathf.FloorToInt(newScore).ToString("D5");
        UpdateHighscore();
    }

    private void UpdateHighscore()
    {
        float currentScore = GameManager.Instance.score;
        float highscore = PlayerPrefs.GetFloat("highscore", 0);

        if (currentScore > highscore)
        {
            highscore = currentScore;
            PlayerPrefs.SetFloat("highscore", highscore);

            // Update highscore text
            UpdateHighscoreText();
        }
    }
    private void UpdateHighscoreText()
    {
        float highscore = PlayerPrefs.GetFloat("highscore", 0);
        highscoreText.text = Mathf.FloorToInt(highscore).ToString("D5");
    }
}
