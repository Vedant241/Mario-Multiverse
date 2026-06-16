using UnityEngine;

public class Obstacale : MonoBehaviour
{
    private float leftedge;

    private void Start()
    {
        leftedge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;

    }

    private void Update()
    {
        transform.position += Vector3.left * DinoGameManager.Instances.gameSpeed * Time.deltaTime;

        if (transform.position.x < leftedge)
        {
            Destroy(gameObject);
        }
    }
}
