using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public string sceneName = "Multiverse";
    public GameObject pauseUI;
    private AudioSource bgMusicAudioSource; // Reference to the background music AudioSource

    void Start()
    {
        Time.timeScale = 1.0f;
        // Get the AudioSource component attached to the background music GameObject
        bgMusicAudioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        pauseUI.SetActive(false);
    }

    public void pauseButton()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        // Pause the background music
        bgMusicAudioSource.Pause();
    }

    public void continueButton()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        // Resume the background music
        bgMusicAudioSource.UnPause();
    }

    public void restartButton()
    {
        if(sceneName == "Mario")
        {
            MarioGameManager.Instance.NewGame();
        }
        if (sceneName == "Pacman")
        {
            P_GameManager.Instance.NewGame();
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void exittoManinMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
