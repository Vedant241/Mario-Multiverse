using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GoombaMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public Rigidbody2D rb;
    public Animator animator;
    private bool canMove = true;
    public Movement marioMovement;
    public float direction = -1f;

    public CircleCollider2D Collider;

    public AudioSource audioSource;
    public AudioClip die;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        marioMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
    #if UNITY_EDITOR
        enabled = !EditorApplication.isPaused;
    #else
        enabled = true;
    #endif
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rb.WakeUp();
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        rb.Sleep();
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
        if (collision.collider.CompareTag("Player") && (marioMovement.isJumping))
        {
            if (collision.relativeVelocity.y < 0f)
            {
                audioSource.PlayOneShot(die);
                animator.SetBool("Dead", true);
                canMove = false;
                Destroy(gameObject, 0.2f);
            }
        }
        else if(collision.collider.CompareTag("Player") && marioMovement.starpower == true)
        {
            Debug.Log("Dying with staar power");
            rb.velocity = new Vector2(0, 15);
            Quaternion targetRotation = Quaternion.Euler(180,0,0);
            transform.rotation = targetRotation;
            canMove = false;
            audioSource.PlayOneShot(die);
            rb.gravityScale = 5;
            Collider.enabled = false;
        }
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Goomba"))
        {
            direction *= -1f;
        }
    }
}
