using UnityEngine;

public class FoodCollectibleItem : MonoBehaviour
{
    public CollectibleType type; 

    private bool isPlayerNear = false;
    private GameObject player;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            CollectibleManager.Instance.CollectItem();

            if (player != null && player.TryGetComponent<PlayerScript>(out var playerScript))
            {
                playerScript.ShowCollectibleIcon(type);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            player = other.gameObject;
            Debug.Log("Press 'E' to collect the item.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null;
        }
    }
}
