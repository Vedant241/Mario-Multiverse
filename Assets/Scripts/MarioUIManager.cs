using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MarioUIManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI livesText;  
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoinsandLivesText();
    }
    void UpdateCoinsandLivesText()
    {
        coinsText.text = "Coins: " + MarioGameManager.Instance.coins.ToString();
        livesText.text = "Lives: " + MarioGameManager.Instance.lives.ToString();
    }
}
