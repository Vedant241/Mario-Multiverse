using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioGameManager : MonoBehaviour
{
    public int lives;
    public int coins = 0;
    public static MarioGameManager Instance { get; private set; }
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
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        NewGame();
    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;
        SceneManager.LoadScene("Mario");
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
    public void AddCoins()
    {
        coins++;
    }
    private void LoadGameScene()
    {
        SceneManager.LoadScene("Mario");
    }

    public void AddLives()
    {
        lives++;
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