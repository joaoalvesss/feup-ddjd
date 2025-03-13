using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Door : MonoBehaviour
{
    public GameObject codeInputUI; 
    public TMP_InputField inputField;
    public TextMeshProUGUI wrongCodeText;
    public Button checkButton; 
    public Sprite openDoorSprite; 

    private SpriteRenderer spriteRenderer;
    private bool playerInRange = false;
    private string correctCode;
    private Collider2D blockingCollider; 
    private Collider2D triggerCollider; // Reference for the trigger collider

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Find the blocking collider specifically
        blockingCollider = transform.Find("BlockingCollider").GetComponent<Collider2D>();

        // Get the trigger collider attached to the main Door object
        triggerCollider = GetComponent<Collider2D>();

        codeInputUI.SetActive(false); 
        wrongCodeText.gameObject.SetActive(false); 
    }

    private void Update()
    {
        if (codeInputUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            codeInputUI.SetActive(false); 
        }
    }

    public void SetCorrectCode(string code)
    {
        correctCode = code;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerCollider.enabled) // Prevent interaction if the trigger is disabled
        {
            playerInRange = true;
            codeInputUI.SetActive(true);
            inputField.text = "";
            wrongCodeText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            codeInputUI.SetActive(false); 
        }
    }

    public void CheckCode()
    {
        if (inputField.text == correctCode)
        {
            UnlockDoor();
        }
        else
        {
            wrongCodeText.gameObject.SetActive(true); 
        }
    }

    private void UnlockDoor()
    {
        Debug.Log("Door Unlocked!");
        spriteRenderer.sprite = openDoorSprite; 
        codeInputUI.SetActive(false); 
        
        if (blockingCollider != null)
        {
            blockingCollider.enabled = false; // ✅ Disable movement-blocking collider
        }
        else
        {
            Debug.LogError("Blocking Collider not found! Make sure the door has a separate collider named 'BlockingCollider'.");
        }

        if (triggerCollider != null)
        {
            triggerCollider.enabled = false; // ✅ Disable trigger collider so player can't interact again
        }
        else
        {
            Debug.LogError("Trigger Collider not found! Ensure it's attached to the Door object.");
        }
    }
}
