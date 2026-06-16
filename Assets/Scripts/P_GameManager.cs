using UnityEngine;
using UnityEngine.UI;

public class P_GameManager : MonoBehaviour
{
    public static P_GameManager Instance { get; private set; }

    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private Pacman pacman;
    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private int ghostMultiplier = 1;
    private int lives = 3;
    private int score = 0;

    public int Lives => lives;
    public int Score => score;

    public AudioSource audioSource { get; private set; }
    public AudioClip chomp;
    public AudioClip ghostEat;
    public AudioClip death;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DestroyGameManager();
        }
        if(MarioGameManager.Instance != null)
        {
            MarioGameManager.Instance.DestroyGameManager();
        }
        if(DonkeyGameManager.Instance != null) 
        {
            DonkeyGameManager.Instance.DestroyGameManager();
        }
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        NewGame();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        gameOverText.enabled = false;

        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }
        pacman.ResetState();
    }

    private void GameOver()
    {
        gameOverText.enabled = true;

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }
        pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PacmanEaten()
    {
        audioSource.PlayOneShot(death);
        pacman.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(ResetState), 3f);
        }
        else
        {
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        audioSource.PlayOneShot(ghostEat);
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);

        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);
        audioSource.PlayOneShot(chomp);
        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        audioSource.PlayOneShot(chomp);
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
    public void DestroyGameManager()
    {
        if (Instance == this)
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
}
