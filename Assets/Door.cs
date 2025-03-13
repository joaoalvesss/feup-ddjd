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
    private Collider2D triggerCollider; 
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        blockingCollider = transform.Find("BlockingCollider").GetComponent<Collider2D>();
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
        if (other.CompareTag("Player") && triggerCollider.enabled) 
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
            blockingCollider.enabled = false; 
        }
        else
        {
            Debug.LogError("Blocking Collider not found! Make sure the door has a separate collider named 'BlockingCollider'.");
        }

        if (triggerCollider != null)
        {
            triggerCollider.enabled = false; 
        }
        else
        {
            Debug.LogError("Trigger Collider not found! Ensure it's attached to the Door object.");
        }
    }
}
