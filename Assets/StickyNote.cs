using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI

public class StickyNote : MonoBehaviour
{
    public GameObject stickyNoteUI;
    public TextMeshProUGUI codeText;
    private bool playerInRange = false;
    private string code;

    public Door linkedDoor; // Reference to the door this note unlocks

    private void Start()
    {
        code = Random.Range(1000, 9999).ToString(); // Generate a random 4-digit code
        codeText.text = "Code: " + code;

        if (linkedDoor != null)
        {
            linkedDoor.SetCorrectCode(code); // Send the code to the door
        }

        stickyNoteUI.SetActive(false);
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
            stickyNoteUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            stickyNoteUI.SetActive(true);
        }

        if (stickyNoteUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            stickyNoteUI.SetActive(false);
        }
    }
}

