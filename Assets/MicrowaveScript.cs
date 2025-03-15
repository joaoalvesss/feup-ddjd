using UnityEngine;

public class Microwave : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Change "U" to whatever key you want
        {
            CollectibleManager.Instance.UseUnlockableItem();
        }
    }
}