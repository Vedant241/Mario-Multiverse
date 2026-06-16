using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DonkeyGameManager : MonoBehaviour
{
    public int lives;
    public static DonkeyGameManager Instance { get; private set; }

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
        if (MarioGameManager.Instance != null)
        {
            MarioGameManager.Instance.DestroyGameManager();
        }
        if (P_GameManager.Instance != null)
        {
            P_GameManager.Instance.DestroyGameManager();
        }
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DestroyGameManager();
        }
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        NewGame();
    }

    public void NewGame()
    {
        lives = 3;
        SceneManager.LoadScene("Level 1");
    }

    public void GameFailed()
    {
        lives--;

        if (lives <= 0)
        {
            NewGame();
        }
        else
        {
            Invoke(nameof(LoadGameScene), 2);
        }
    }

    private void Update()
    {

    }
    private void LoadGameScene()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void DestroyGameManager()
    {
        if (Instance == this)
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
    public void LevelFailed()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LevelComplete()
    {
        lives--;
        if (lives <= 0)
        {
            SceneManager.LoadScene("Level 1");
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}