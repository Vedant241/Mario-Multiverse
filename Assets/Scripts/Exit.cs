using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string Scenename = "GameSelectionScene";
    public void ExitButton()
    {
        SceneManager.LoadScene(Scenename);
    }
}
