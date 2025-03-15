using UnityEngine;

public class CleaningLady : MonoBehaviour
{
    private bool isPlayerNear = false;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player talking to Cleaning Lady");
            TalkToPlayer();
        }

        // Hide text when player presses E again
        if (Input.GetKeyDown(KeyCode.E) && UIManager.Instance.dialogueBox.activeSelf)
        {
            UIManager.Instance.HideDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            UIManager.Instance.ShowDialogue("Press 'E' to talk to the cleaning lady.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            UIManager.Instance.HideDialogue();
        }
    }

    private void TalkToPlayer()
    {
        int count = CollectibleManager.Instance.CollectedCount;
        string message = "";

        
        if (count < 3)
        {
            Debug.Log("Cleaning Lady: 'I'm soooo hungry right now, I would give anything for a lunch!'");
	    message = "I'm soooo hungry right now, I would give anything for a lunch!";
        }
        else if (count == 3)
        {
            Debug.Log("Cleaning Lady: 'That's no good, everything is cold!'");
	    message = "That's no good, everything is cold!";
        }
        else if (count == 4)
        {
            Debug.Log("Cleaning Lady: 'Ahhh, this tastes so good. Here, take this key!'");
	    message = "Ahhh, this tastes so good. Here, take this key!";
            GivePlayerReward();
        }

        UIManager.Instance.ShowDialogue(message);
    }

    private void GivePlayerReward()
    {
        Debug.Log("Player received the special object from the cleaning lady!");
        // Add logic to give the player the item
    }
}