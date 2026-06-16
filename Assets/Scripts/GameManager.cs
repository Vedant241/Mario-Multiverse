using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float score;
    public static GameManager Instance { get; private set; }
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
        if(MarioGameManager.Instance != null)
        {
            MarioGameManager.Instance.DestroyGameManager();
        }
        if(P_GameManager.Instance != null)
        {
            P_GameManager.Instance.DestroyGameManager();
        }
        if(DonkeyGameManager.Instance != null)
        {
            DonkeyGameManager.Instance.DestroyGameManager();
        }  
        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        SceneManager.LoadScene("MultiVerse");
    }
    private void SceneLoadDelay()
    {
        score = 0;
        SceneManager.LoadScene("MultiVerse");
    }
    public void GameFailed()
    {
        Invoke("SceneLoadDelay", 2f);
    }
    private void Update()
    {
        if (M_UiManager.Instance != null)
        {
            float speedIncrease = M_UiManager.Instance.gameSpeedIncrease * Time.deltaTime;
            M_UiManager.Instance.gameSpeed += speedIncrease;

            score += M_UiManager.Instance.gameSpeed * Time.deltaTime;
            M_UiManager.Instance.UpdateScore(score);
        }
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
