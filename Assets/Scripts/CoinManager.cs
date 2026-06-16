using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    private AudioSource audioSource; // Remove the public keyword

    public AudioClip coin;

    // Use Awake to ensure that the AudioSource is obtained before any other code runs
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // Add a check to create an AudioSource if one doesn't exist on the GameObject
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MainManager.Instance.AddCoins(1);
            MarioGameManager.Instance.AddCoins();
            audioSource.PlayOneShot(coin); // Use PlayOneShot for sound effects
            Destroy(gameObject);
        }
    }
}
