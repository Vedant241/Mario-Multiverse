using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Barrel : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float speed = 1f;

    private MarioMovement mm;

    private void Awake()
    {
        mm = GameObject.FindGameObjectWithTag("Player").GetComponent<MarioMovement>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rigidbody.AddForce(-collision.transform.right * speed, ForceMode2D.Impulse);
        }
        if (collision.collider.CompareTag("Player") && (mm.isJumping) && (collision.relativeVelocity.y < 0f))
        {
            Destroy(gameObject);
        }
    }
}
