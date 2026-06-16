using System.Collections;
using UnityEngine;
using TouchControlsKit;
public class Movement : MonoBehaviour
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
    public bool starpower;

    private float currentSpeed;
    public float starPowerSpeed = 7f;

    public AudioClip invincible;
    public AudioClip die;
    public AudioClip jumpSmall;
    public AudioClip jumpBig;
    public AudioClip powerUp;
    public AudioClip powerDown;
    public AudioSource audioSource;
    public AudioSource MainthemeaudioSource2;

    private new Camera camera;
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
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRender = smallRender;
        itemType = Mario.small;
        activeRender = smallRender;
        bigRender.enabled = false;
        smallRender.enabled = true;
        animator = smallMarioAnimator;
        currentSpeed = moveSpeed;
        Application.targetFrameRate = 60;
        audioSource = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        Application.targetFrameRate = 120;
        camera = Camera.main;
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        float horizontalInput = TCKInput.GetAction("moveRight", EActionEvent.Press) ? 1f : TCKInput.GetAction("moveLeft", EActionEvent.Press) ? -1f : 0f;
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float leftLimit = leftEdge.x + activeRender.bounds.size.x / 2f;
        float rightLimit = rightEdge.x - activeRender.bounds.size.x / 2f;
        if (transform.position.x <= leftLimit && moveDirection.x < 0)
        {
            moveDirection.x = 0;
        }
        // Check if Mario is at the right edge and trying to move right
        if (transform.position.x >= rightLimit && moveDirection.x > 0)
        {
            moveDirection.x = 0;
        }
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        if (moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Running", true);
            animator.SetBool("Idle", false); // Set Idle to false when running
        }
        else if (moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("Running", true);
            animator.SetBool("Idle", false); // Set Idle to false when running
        }
        else
        {
            animator.SetBool("Running", false);
            animator.SetBool("Idle", isGrounded); // Set Idle to isGrounded when not running
        }

        if (isGrounded && TCKInput.GetAction("Jump", EActionEvent.Down) && !isJumping)
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
        if (activeRender == smallRender)
        {
            itemType = Mario.small;
        }
        else if (activeRender == bigRender)
        {
            itemType = Mario.Big;
        }
        if (TCKInput.GetAction("Jump", EActionEvent.Up))
        {
            ReleaseJump();
        }
        if (starpower)
        {
            currentSpeed = starPowerSpeed;
        }
        else if (!starpower)
        {
            currentSpeed = moveSpeed;
        }
        if (!isGrounded)
        {
            animator.SetBool("Jumping", true);
            animator.SetBool("Running", false);
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
            MarioGameManager.Instance.GameFailed();
            animator.SetBool("Jumping", false);
        }
        else if ((collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Goomba")) && big == true && !starpower && !isJumping)
        {
            Shrink();
        }
        if (collision.collider.CompareTag("Mushroom"))
        {
            Grow();
        }
        if (collision.collider.CompareTag("Starman"))
        {
            Starpower();
        }
        if ((collision.collider.CompareTag("Goomba") || collision.collider.CompareTag("Enemy")) && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 6);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mushroom"))
        {
            Grow();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Starman"))
        {
            Starpower();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("1UpMushroom"))
        {
            MarioGameManager.Instance.AddLives();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("CollisionCheck"))
        {
            MarioGameManager.Instance.GameFailed();
            audioSource.PlayOneShot(die);
        }
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
    public void Grow()
    {
        audioSource.PlayOneShot(powerUp);
        smallRender.enabled = false;
        bigRender.enabled = true;
        activeRender = bigRender;
        animator = bigMarioAnimator;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
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
        StartCoroutine(StarpowerAnimation());
    }
    private IEnumerator StarpowerAnimation()
    {
        MainthemeaudioSource2.Pause();
        audioSource.PlayOneShot(invincible);
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
        MainthemeaudioSource2.UnPause();
    }
}