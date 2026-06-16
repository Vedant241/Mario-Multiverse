
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController charecter;
    private Vector3 direction;

    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;


    public AudioSource audioSource;
    public AudioClip jump;
    private void Awake()
    {
        charecter = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update ()
    {
        direction += Vector3.down * gravity * Time.deltaTime;
 
         if (charecter.isGrounded)
         {
            direction = Vector3.down;

            if (Input.GetButton("Jump") || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                direction = Vector3.up * jumpForce;
                audioSource.PlayOneShot(jump);
            }
         }

         charecter.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstical"))
        {
            DinoGameManager.Instances.GameOver();
        }
    }

}