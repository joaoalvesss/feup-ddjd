using UnityEngine;

public class VisionCone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected! Alert triggered.");
            // Add your alert logic here (e.g., start an alarm)
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left vision area.");
        }
    }
}
