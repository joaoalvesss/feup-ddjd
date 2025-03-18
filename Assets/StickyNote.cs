using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro UI

public class StickyNote : MonoBehaviour
{
    public GameObject stickyNoteUI;
    public TextMeshProUGUI codeText;
    private bool playerInRange = false;
    private string code;

    public Door linkedDoor; 

    private void Start()
    {
        code = Random.Range(1000, 9999).ToString(); 
        codeText.text = "Code: " + code;

        if (linkedDoor != null)
        {
            linkedDoor.SetCorrectCode(code);
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

