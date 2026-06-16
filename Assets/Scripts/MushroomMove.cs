
using System.Collections;
using UnityEngine;
public class MushroomMove : MonoBehaviour
{
    public Rigidbody2D rb;
    private float direction = 1f;

    public float speed = 3f;
    public BlockItem blockItem;
    private void Update()
    {
        if (blockItem.hasMoved)
        {
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            direction *= -1;
        }
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
