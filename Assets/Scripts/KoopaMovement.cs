using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public Rigidbody2D rb;
    private bool canMove = true;
    public float direction = -1f;
    public CircleCollider2D Collider;

    Animator animator;

    public Movement mm;

    private int playerCollisions = 0;

    public AudioSource audioSource;
    public AudioClip die;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            direction *= -1f;
        }
        if (collision.collider.CompareTag("Player") && (mm.isJumping))
        {
            animator.SetBool("Dead", true);
            canMove = false;
            audioSource.PlayOneShot(die);
            rb.velocity = Vector2.zero;
            StartCoroutine(Restart());
            playerCollisions++;
        }
        else if (collision.collider.CompareTag("Player") && mm.starpower == true)
        {
            Debug.Log("Dying with star power");
            rb.velocity = new Vector2(0, 15);
            Quaternion targetRotation = Quaternion.Euler(180, 0, 0);
            transform.rotation = targetRotation;
            audioSource.PlayOneShot(die);
            canMove = false;
            rb.gravityScale = 5;
            Collider.enabled = false;
        }
        if (collision.collider.CompareTag("Player") && mm.isJumping && !canMove && playerCollisions >= 2)
        {
            rb.velocity = new Vector2(20 * direction, rb.velocity.y);
            playerCollisions = 0;
        }
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(4f);
        canMove = true;
        animator.SetBool("Dead", false);
    }
}
