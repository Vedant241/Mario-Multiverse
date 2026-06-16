using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    public int points = 10;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.GetComponent<AudioSource>();
    }
    protected virtual void Eat()
    {
        P_GameManager.Instance.PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }

}