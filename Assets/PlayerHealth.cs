using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxLives = 3f; 
    private float currentLives;

    private void Awake()
    {
        currentLives = maxLives; // Initialize health on awake
    }

    private void Start()
    {
        UIManager.Instance.UpdateLives(currentLives); // Ensure UI updates at the start
    }

    private void OnEnable()
    {
        UIManager.Instance.UpdateLives(currentLives); // Update UI every time player respawns
    }


    public void TakeDamage(float damage)
    {
        currentLives -= damage;
        UIManager.Instance.UpdateLives(currentLives); // UI Update BEFORE checking Game Over

        Debug.Log("Lives Left: " + currentLives);

        if (currentLives <= 0)
        {
            Debug.Log("Game Over!");
            UIManager.Instance.ShowGameOver(); 
            Invoke(nameof(DisablePlayer), 1f); // Wait 1 second before deactivating player
        }
    }

    private void DisablePlayer()
    {
        gameObject.SetActive(false); 
    }

    public void ResetHealth()
    {
        currentLives = maxLives;
        UIManager.Instance.UpdateLives(currentLives); 
        gameObject.SetActive(true); 
        Debug.Log("Health Reset: " + currentLives);
    }

    public float GetRemainingLives()
    {
        return currentLives;
    }
}
