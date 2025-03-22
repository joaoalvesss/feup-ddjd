using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeycardDoor : MonoBehaviour
{
    public Transform exitPoint;
    private bool playerInRange = false;
    private GameObject player;
    public Text doorLockedText; // Assign in Unity

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
            if (doorLockedText != null)
            {
                doorLockedText.gameObject.SetActive(false); // Hide text when player leaves
            }
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
            else
            {
                ShowDoorLockedMessage();
            }
        }
    }

    private void ShowDoorLockedMessage()
    {
        if (doorLockedText != null)
        {
            doorLockedText.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(HideMessageAfterDelay());
        }
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Adjust duration as needed
        doorLockedText.gameObject.SetActive(false);
    }
}
