using UnityEngine;

public class SideDoor : MonoBehaviour
{
    public Transform exitPoint; // Set this to where the player should appear after using the stairs
    private bool playerInRange = false;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            player.transform.position = exitPoint.position + new Vector3(0f, -1.32f, 0f); // Teleport the player
        }
    }
}
