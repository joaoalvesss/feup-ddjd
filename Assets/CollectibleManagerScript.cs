using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private int collectedCount = 0;
    public int CollectedCount => collectedCount;

    public Text unlockableItemText; // Assign this in Unity

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectItem()
    {
        if (collectedCount < 3) 
        {
            collectedCount++;
            Debug.Log("Collected: " + collectedCount);
        }
    }

    public void UseUnlockableItem()
    {
        if (collectedCount == 3) 
        {
	    // Show text and start delay
            if (unlockableItemText != null)
            {
                unlockableItemText.gameObject.SetActive(true);
            }

            StartCoroutine(HideMessageAfterDelay());
        }
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        
        if (unlockableItemText != null)
        {
            unlockableItemText.gameObject.SetActive(false);
        }
        collectedCount++;
        Debug.Log("Used unlockable item! Count is now: " + collectedCount);
    }

    public void GetKey()
    {
        if (collectedCount == 4) 
        {
            collectedCount++;
            Debug.Log("Got key! Count is now: " + collectedCount);
        }
    }
}
