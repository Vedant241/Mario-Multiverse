using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public int totalCoins = 0;
    public int starPower = 0;
    public int big = 0;

    [SerializeField]private MainMenuManager mainMenuManager;
    // Static reference to the instance of MainMenuManager
    public static MainManager Instance;


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

        LoadTotalCoins();
        LoadTotalStarPower();
        LoadTotalBig();
    }
    private void LoadTotalBig()
    {
        starPower = PlayerPrefs.GetInt("big", 0);
    }
    private void LoadTotalStarPower()
    {
        starPower = PlayerPrefs.GetInt("starPower", 0);
    }
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        if (mainMenuManager != null)
        {
            mainMenuManager.UpdateCoinsUI();
        }
        SaveTotalCoins();
    }
    public void RemoveCoins(int amount)
    {
        totalCoins -= amount;
        if (totalCoins < 0)
        {
            totalCoins = 0;
        }
        if (mainMenuManager != null)
        {
            mainMenuManager.UpdateCoinsUI();
        }
        SaveTotalCoins();
    }
    private void OnApplicationQuit()
    {
        SaveTotalCoins();
        SaveTotalStars();
        SaveTotalBig();
    }
    private void LoadTotalCoins()
    {
        totalCoins = PlayerPrefs.GetInt("totalCoins", 0);
    }

    private void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("totalCoins", totalCoins);
        PlayerPrefs.Save();
    }
    private void SaveTotalBig()
    {
        PlayerPrefs.SetInt("big", big);
        PlayerPrefs.Save();
    }
    private void SaveTotalStars()
    {
        PlayerPrefs.SetInt("starPower", starPower);
        PlayerPrefs.Save();
    }
    public void AddStars(int amount)
    {
        starPower += amount;
        SaveTotalStars();
    }
    public void RemoveStars(int amount)
    {
        if(starPower < 0)
        {
            starPower = 0;
        }
        starPower -= amount;
        SaveTotalStars();
    }
    public void AddBig(int amount)
    {
        big += amount;
        SaveTotalBig();
    }
    public void RemoveBig(int amount)
    {
        if(big < 0) 
        {
            big = 0;
        }
        big -= amount;
        SaveTotalBig();
    }
}
