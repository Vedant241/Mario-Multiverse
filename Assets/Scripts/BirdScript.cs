using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    AudioSource source;

    public GameObject flyButton;
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (birdIsAlive)
        {
            // Handle keyboard input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Flap();
            }
        }
        if(!birdIsAlive) 
        {
            flyButton.SetActive(false);
        }
    }

    public void Flap()
    {
        if (birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
    }

    private void OnBecameInvisible()
    {
        logic.gameOver();
        birdIsAlive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        source.Play();
        birdIsAlive = false;
    }
}