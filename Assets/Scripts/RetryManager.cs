using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class RetryManager : MonoBehaviour
{
    public Button retryButton;
    public Movement m;

    private String sceneName = "Mario";
    // Start is called before the first frame update
    void Start()
    {
        retryButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RetryButton()
    {
        if (m.isDead)
        {
            SceneManager.LoadScene(sceneName);
        }   
    }
}
