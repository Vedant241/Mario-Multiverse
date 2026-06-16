using UnityEngine;

public class M_Obstacale : MonoBehaviour
{
    private float leftedge;
    private Animator animator;

    private MarioMovement mm;
    private Collider2D Collider;
    private Rigidbody2D rb;

    private void Start()
    {
        Collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        leftedge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
        mm = GameObject.FindGameObjectWithTag("Player").GetComponent<MarioMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position += Vector3.left * M_UiManager.Instance.gameSpeed * Time.deltaTime;

        if (transform.position.x < leftedge)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Get the collision normal
            Vector2 collisionNormal = collision.contacts[0].normal;

            // Check if the collision is from above or below
            float dotProduct = Vector2.Dot(collisionNormal, Vector2.up);

            if (mm.isJumping && dotProduct < 0)
            {
                MainManager.Instance.AddCoins(1);
                animator.SetBool("Dead", true);
                Destroy(gameObject, 0.2f);
            }
            else if (mm.starpower && dotProduct < 0)
            {
                Debug.Log("Dying with star power");
                rb.velocity = new Vector2(0, 15);
                Quaternion targetRotation = Quaternion.Euler(180, 0, 0);
                transform.rotation = targetRotation;
                rb.gravityScale = 5;
                Collider.enabled = false;
            }
        }
    }
}
