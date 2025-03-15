using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        dialogueBox.SetActive(false); // Hide dialogue initially
    }

    public void ShowDialogue(string message)
    {
        Debug.Log("Showing dialogue box");
        dialogueBox.SetActive(true);
        dialogueText.text = message;
    }

    public void HideDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
