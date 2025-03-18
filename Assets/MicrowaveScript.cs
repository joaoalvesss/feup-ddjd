using UnityEngine;

public class Microwave : MonoBehaviour
{
    private bool isPlayerNear = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            CollectibleManager.Instance.UseUnlockableItem();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Press 'E' to collect the item.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}