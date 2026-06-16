using Unity.VisualScripting;
using UnityEngine;

public class F_GameManager : MonoBehaviour
{
    public GameObject taptoStartText;
    private void Start()
    {
        Time.timeScale = 0f;
        taptoStartText.SetActive(true);
    }
    private void Update()
    {
        if (Input.touchCount > 0) 
        {
            Time.timeScale = 1.0f;
            taptoStartText.SetActive(false);
        }
    }
}
