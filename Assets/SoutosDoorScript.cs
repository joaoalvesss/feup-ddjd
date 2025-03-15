using UnityEngine;

public class SoutosDoor : MonoBehaviour
{
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;

    private SpriteRenderer spriteRenderer;
    public BoxCollider2D solidCollider; // The blocking collider
    private bool isUnlocked = false;
    private bool isOpen = false;
    private bool playerNearby = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        solidCollider = GetComponent<BoxCollider2D>(); // The first (solid) collider

        spriteRenderer.sprite = closedDoorSprite;
    }

    private void Update()
    {
        // Unlock the door when collectibles reach 5
        if (!isUnlocked && CollectibleManager.Instance.CollectedCount >= 5)
        {
            isUnlocked = true;
            Debug.Log("Door is now unlocked!");
        }

        // Player can only open the door when nearby and unlocked
        if (isUnlocked && !isOpen && playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;

	if (openDoorSprite != null && spriteRenderer != null)
    	{
    	    spriteRenderer.sprite = openDoorSprite; // Change sprite
	    transform.position += new Vector3(-2.25f, 0f, 0f);
    	    Debug.Log("Door is now open! Sprite changed.");
    	}
    	else
    	{
    	    Debug.LogError("Open door sprite or SpriteRenderer is missing!");
    	}

        Debug.Log("Door is now open!");
        solidCollider.enabled = false; // Disables the blocking collider so the player can pass
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player is near the door.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Debug.Log("Player left the door area.");
        }
    }
}
