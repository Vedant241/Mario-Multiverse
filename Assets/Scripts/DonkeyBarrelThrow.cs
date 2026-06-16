using UnityEngine;

public class DonkeyBarrelThrow : MonoBehaviour
{
    public Transform raycastOrigin;  // The position from which the rays will be cast
    public LayerMask collisionLayer;  // The layer(s) on which the rays will detect collisions
    public GameObject barrelPrefab;
    public float barrelForce = 10f;  // Adjust the force applied to the barrel
    public float throwingRange = 5f;  // The range within which the barrel will be thrown

    private bool canThrowBarrel = true;
    private Vector2 throwDirection; // Store the throw direction

    private void Update()
    {
        // Shoot ray to the right
        RaycastHit2D hitRight = Physics2D.Raycast(raycastOrigin.position, Vector2.right, throwingRange, collisionLayer);
        Debug.DrawRay(raycastOrigin.position, Vector2.right * throwingRange, Color.red);

        // Shoot ray to the left
        RaycastHit2D hitLeft = Physics2D.Raycast(raycastOrigin.position, Vector2.left, throwingRange, collisionLayer);
        Debug.DrawRay(raycastOrigin.position, Vector2.left * throwingRange, Color.blue);

        if (hitRight.collider != null)
        {
            // Collision on the right side
            Debug.Log("Hit something on the right: " + hitRight.collider.name);
            throwDirection = Vector2.right; // Set the throw direction
            ThrowBarrelAfterDelay(2f); // Start throwing barrel after 2 seconds
        }

        if (hitLeft.collider != null)
        {
            // Collision on the left side
            Debug.Log("Hit something on the left: " + hitLeft.collider.name);
            throwDirection = Vector2.left; // Set the throw direction
            ThrowBarrelAfterDelay(2f); // Start throwing barrel after 2 seconds
        }
    }

    void ThrowBarrelAfterDelay(float delay)
    {
        if (canThrowBarrel)
        {
            canThrowBarrel = false;
            Invoke("ThrowBarrel", delay);
            Invoke(nameof(ResetCanThrow), delay);
        }
    }

    void ThrowBarrel()
    {
        GameObject newBarrel = Instantiate(barrelPrefab, transform.position, Quaternion.identity);

        // Get the rigidbody of the barrel and apply force to make it move in the specified direction
        Rigidbody2D barrelRigidbody = newBarrel.GetComponent<Rigidbody2D>();
        barrelRigidbody.AddForce(throwDirection * barrelForce, ForceMode2D.Impulse);

        Debug.Log("Threw barrel");
    }

    void ResetCanThrow()
    {
        canThrowBarrel = true;
    }
}
