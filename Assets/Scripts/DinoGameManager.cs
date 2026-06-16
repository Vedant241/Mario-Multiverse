using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DinoGameManager : MonoBehaviour
{
    public static DinoGameManager Instances { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOvertext;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public Button retryButton;

    public AudioClip milestoneSound;
    public AudioClip dieSound;
    private AudioSource audioSource;
    private int lastMilestone = 0;

    private Player player;
    private ObstacleSpawner spawner;

    private float score;

    private void Awake()
    {
        if (Instances == null)
        {
            Instances = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instances == this)
        {
            Instances = null;
        }
    }

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        spawner = FindAnyObjectByType<ObstacleSpawner>();

        audioSource = GetComponent<AudioSource>();

        NewGame();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.targetFrameRate = 120;
    }

    public void NewGame()
    {
        Obstacale[] obstacles = FindObjectsOfType<Obstacale>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOvertext.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        audioSource.Stop(); // Stop any playing sound when starting a new game

        UpdateHighscore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOvertext.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        PlayDieSound();
        UpdateHighscore();
    }

    private void PlayDieSound()
    {
        if (dieSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dieSound);
        }
    }

    private void Update()
    {
        float speedIncrease = gameSpeedIncrease * Time.deltaTime;
        gameSpeed += speedIncrease;

        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        CheckScoreMilestones();
    }

    private void CheckScoreMilestones()
    {
        int currentMilestone = Mathf.FloorToInt(score / 100) * 100;

        if (currentMilestone > lastMilestone)
        {
            PlayMilestoneSound();
            lastMilestone = currentMilestone;
        }
    }

    private void PlayMilestoneSound()
    {
        Debug.Log("Playing milestone sound");
        if (milestoneSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(milestoneSound);
        }
    }

    private void UpdateHighscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        highscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
