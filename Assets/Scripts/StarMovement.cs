using UnityEngine;

public class StarMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float upspeed = 0.05f;
    public CircleCollider2D circleCollider;
    public float targetY = 1.0f;
    private bool shouldMove = true;
    private float direction = 1f;
    public float moveSpeed = 3f;
    public float bounceForce = 1.0f;

    private void Start()
    {
        rb.gravityScale = 0;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
    }

    private void Update()
    {
        if (shouldMove)
        {
            transform.Translate(Vector2.up * upspeed * Time.deltaTime);
        }

        if (transform.position.y >= targetY)
        {
            shouldMove = false;
            rb.gravityScale = 1;
            circleCollider.enabled = true;
            rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            direction *= -1f;
        }
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Ground"))
        {
            Vector2 newVelocity = new Vector2(rb.velocity.x, bounceForce);
            rb.velocity = newVelocity;
        }
    }
}
