using UnityEngine;

public class KeycardDoor: MonoBehaviour
{
    public Transform exitPoint; 
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
        if (playerInRange && Input.GetKeyDown(KeyCode.W))
        {
	    PlayerScript playerScript = player.GetComponent<PlayerScript>();
	    if (playerScript != null && playerScript.HasCard())
	    {
            playerScript.DropCard();
            player.transform.position = exitPoint.position + new Vector3(0f, -1.07f, 0f); 
	    }
        }
    }
}
