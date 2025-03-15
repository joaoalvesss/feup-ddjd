using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private int collectedCount = 0;
    public int CollectedCount => collectedCount;

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
            collectedCount++;
            Debug.Log("Used unlockable item! Count is now: " + collectedCount);
        }
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
