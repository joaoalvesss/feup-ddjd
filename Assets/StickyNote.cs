using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI

public class StickyNote : MonoBehaviour
{
    public GameObject stickyNoteUI; // Reference to the UI Panel
    public TextMeshProUGUI codeText; // Text for the 4-digit code
    private bool playerInRange = false;
    private string code;

    private void Start()
    {
        // Generate a random 4-digit code (for example: 1234)
        code = Random.Range(1000, 9999).ToString();
        codeText.text = "Code: " + code; // Display the code
        stickyNoteUI.SetActive(false); // Hide the note at the start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            stickyNoteUI.SetActive(false); // Close the note if the player leaves
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            stickyNoteUI.SetActive(true); // Show the note
        }

        if (stickyNoteUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            stickyNoteUI.SetActive(false); // Hide the note when ESC is pressed
        }
    }
}
