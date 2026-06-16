using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnEnable()
    {
        GetComponent<GhostChase>().Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        // Do nothing while the ghost is frightened
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            // Find the available direction that moves closest to pacman
            foreach (Vector2 availableDirection in node.availableDirection)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = Vector3.Distance(newPosition, ghost.target.position);

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            Debug.Log("Chosen Direction: " + direction); // Add this line for debugging

            ghost.movement.SetDirection(direction);
        }
    }
}
