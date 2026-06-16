using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class GameSelection : MonoBehaviour
{
    public string mario = "Mario";
    public string donkey = "DonkeyKong";
    public string Flappy = "Flappy";
    public string Dino = "Dino";
    public string Pacman = "Pacman";
    public string Multiverse = "MultiversePreload";
    public void MarioSelect()
    {
        if(MarioGameManager.Instance != null)
        {
            MarioGameManager.Instance.DestroyGameManager();
            SceneManager.LoadScene(mario);
        }
        else 
        {
            SceneManager.LoadScene(mario);
        }
    }
    public void DonkeyKongSelect()
    {
        if (DonkeyGameManager.Instance != null)
        {
            DonkeyGameManager.Instance.DestroyGameManager();
            SceneManager.LoadScene(donkey);
        }
        else
        {
            SceneManager.LoadScene(donkey);
        }
    }
    public void FlappyBirdSelect()
    {
        SceneManager.LoadScene(Flappy);
    }
    public void DinoSelect()
    {
        SceneManager.LoadScene(Dino);
    }
    public void PacmanSelect()
    {
        if(P_GameManager.Instance != null) 
        {
            P_GameManager.Instance.DestroyGameManager();
            SceneManager.LoadScene(Pacman);
        }
        else 
        {
            SceneManager.LoadScene(Pacman);
        }
    }
    public void MultiverseSelect()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DestroyGameManager();
            SceneManager.LoadScene(Multiverse);
        }
        else
        {
            SceneManager.LoadScene(Multiverse);
        }
    }
    private void Start()
    {
        UnityEngine.Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
