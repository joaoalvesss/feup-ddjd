using UnityEngine;

public class CleaningLady : MonoBehaviour
{
    private bool isPlayerNear = false;
    private bool isTalking = false;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTalking)
            {
                TalkToPlayer();
                isTalking = true;
            }
            else
            {
                UIManager.Instance.HideDialogue();
                isTalking = false;
            }
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
            isTalking = false; 
            UIManager.Instance.HideDialogue();
        }
    }

    private void TalkToPlayer()
    {
        int count = CollectibleManager.Instance.CollectedCount;
        string message = "";

        if (count < 3)
        {
            message = "I'm soooo hungry right now, I would give anything for a lunch!";
        }
        else if (count == 3)
        {
            message = "That's no good, everything is cold!";
        }
        else if (count == 4)
        {
            message = "Ahhh, this tastes so good. Here, take this key!";
            GivePlayerReward();
        }

        UIManager.Instance.ShowDialogue(message);
    }

    private void GivePlayerReward()
    {
        Debug.Log("Player received the special object from the cleaning lady!");

        CollectibleManager.Instance.GetKey();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && player.TryGetComponent<PlayerScript>(out var playerScript))
        {
            playerScript.HideAllCollectibleIcons();
        }
    }

}
