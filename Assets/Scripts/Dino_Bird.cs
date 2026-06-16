using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dino_Bird : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
