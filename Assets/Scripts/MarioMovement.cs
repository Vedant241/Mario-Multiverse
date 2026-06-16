using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
public class MarioMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    public bool isGrounded;
    public Rigidbody2D rb;
    public Animator animator;
    public bool isJumping;
    public bool isDead = false;

    public CapsuleCollider2D capsuleCollider;

    public bool big => bigRender.enabled;
    public Mario itemType;
    public SpriteRenderer smallRender;
    public SpriteRenderer bigRender;
    public SpriteRenderer activeRender;

    public Animator smallMarioAnimator;
    public Animator bigMarioAnimator;

    public SpriteRenderer spriteRenderer { get; private set; }

    private float currentSpeed;
    public float starPowerSpeed = 7f;

    public AudioClip die;
    public AudioClip jumpSmall;
    public AudioClip jumpBig;
    public AudioClip powerUp;
    public AudioClip powerDown;
    public AudioSource audioSource;

    private new Camera camera;

    public bool starpower;

    public Button starButton;
    public Button bigButton;
    public enum Mario
    {
        small,
        Big
    }
    private void OnDisable()
    {
        isJumping = false;
        rb.isKinematic = true;
        capsuleCollider.enabled = false;
        rb.velocity = Vector2.zero;
    }
    private void OnEnable()
    {
        isJumping = false;
        rb.isKinematic = false;
        capsuleCollider.enabled = true;
        rb.velocity = Vector2.zero;
    }
    void Start()
    {
        if (MainManager.Instance.starPower > 0)
        {
            starButton.interactable = true;
        }
        else if(MainManager.Instance.starPower <= 0)
        {
            starButton.interactable = false;
        }
        if (MainManager.Instance.big > 0)
        {
            bigButton.interactable = true;
        }
        else if( MainManager.Instance.big <= 0)
        {
            bigButton.interactable = false;
        }
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRender = smallRender;
        itemType = Mario.small;
        activeRender = smallRender;
        bigRender.enabled = false;
        smallRender.enabled = true;
        animator = smallMarioAnimator;
        currentSpeed = moveSpeed;
        Application.targetFrameRate = 120;
        audioSource = GetComponent<AudioSource>();
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }
    private void Awake()
    {
        camera = Camera.main;
    }
    void Update()
    {
        // Check if grounded and handle jumping
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGrounded && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Jump();
        }
        else if (isGrounded)
        {
            animator.SetBool("Jumping", false);
            isJumping = false;
        }
        if (!isGrounded)
        {
            animator.SetBool("Jumping", true);
            animator.SetBool("Running", false);
            isJumping = true;
        }
        // Update animator based on state
        animator.SetBool("Running", Mathf.Abs(rb.velocity.x) > 0 && isGrounded);
        animator.SetBool("Dead", isDead);

        // Ensure no X-axis movement
        rb.velocity = new Vector2(0, rb.velocity.y);
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        if (MainManager.Instance.big == 0)
        {
            bigButton.interactable = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Goomba")) && isGrounded && big == false && starpower == false)
        {
            audioSource.PlayOneShot(die);
            isDead = true;
            animator.SetBool("Dead", true);
            rb.gravityScale = 1;
            rb.velocity = new Vector2(0, 7);
            capsuleCollider.enabled = false;
            GameManager.Instance.GameFailed();
            animator.SetBool("Jumping", false);
        }
        else if ((collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Goomba")) && big == true && !starpower && !isJumping)
        {
            Shrink();
        }
        if ((collision.collider.CompareTag("Goomba") || collision.collider.CompareTag("Enemy")) && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 6);
        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRender.enabled = !smallRender.enabled;
                bigRender.enabled = !smallRender.enabled;
            }

            yield return null;
        }

        smallRender.enabled = false;
        bigRender.enabled = false;
        activeRender.enabled = true;
    }
    public void Jump()
    {
        if (itemType == Mario.small)
        {
            audioSource.PlayOneShot(jumpSmall);
        }
        else if (itemType == Mario.Big)
        {
            audioSource.PlayOneShot(jumpBig);
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetBool("Jumping", true);
        isJumping = true;
    }
    public void ReleaseJump()
    {
        if (rb.velocity.y > 0)
        {
            // If the player is still moving upwards (jumping), reduce the y-velocity
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        isJumping = false;
    }
    private void OnBecameInvisible()
    {
        FindObjectOfType<GameManager>().GameFailed();
    }
    public void Grow()
    {
        if (MainManager.Instance.big > 0)
        {
            MainManager.Instance.RemoveBig(1);
            audioSource.PlayOneShot(powerUp);
            smallRender.enabled = false;
            bigRender.enabled = true;
            activeRender = bigRender;
            animator = bigMarioAnimator;

            capsuleCollider.size = new Vector2(1f, 2f);
            capsuleCollider.offset = new Vector2(0f, 0.5f);
            if (MainManager.Instance.big == 0)
            {
                bigButton.interactable = false;
            }
            animator.SetBool("Running", true);
            StartCoroutine(ScaleAnimation());
        }
    }
    public void Shrink()
    {
        audioSource.PlayOneShot(powerDown);
        smallRender.enabled = true;
        bigRender.enabled = false;
        activeRender = smallRender;
        animator = smallMarioAnimator;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }
    public void Starpower()
    {
        if (MainManager.Instance.starPower > 0)
        {
            if (MainManager.Instance.starPower == 0)
            {
                starButton.interactable = false;
            }
            MainManager.Instance.RemoveStars(1);
            StartCoroutine(StarpowerAnimation());
        }
    }
    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        float elapsed = 0f;
        float duration = 10f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                activeRender.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }
        activeRender.color = Color.white;
        starpower = false;
    }
}