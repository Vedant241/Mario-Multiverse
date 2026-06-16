using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using Unity.VisualScripting;
[RequireComponent(typeof(P_Movement))]
public class Pacman : MonoBehaviour
{
    [SerializeField]
    private AnimatedSprite deathSequence;
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    private P_Movement movement;
    private new Collider2D collider;

    [SerializeField]private SwipeListener swipeListener;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<P_Movement>();
        collider = GetComponent<Collider2D>();
        spriteRenderer.enabled = true;
    }
    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener (OnSwipe) ;
    }
    private void OnSwipe(string swipe)
    {
        Debug.Log(swipe);
        switch (swipe)
        {
            case "Left":
                movement.SetDirection(Vector2.left);
                break;
            case "Up":
                movement.SetDirection(Vector2.up);
                break;
            case "Down":
                movement.SetDirection(Vector2.down);
                break;
            case "Right":
                movement.SetDirection(Vector2.right);
                break;
        }
    }
    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveAllListeners();
    }
    private void Update()
    {
        // Set the new direction based on the current input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right);
        }

        // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        collider.enabled = true;
        deathSequence.enabled = false;
        movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        collider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.Restart();
    }

}