using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxLives = 3f; 
    private float currentLives;

    private void Start()
    {
        currentLives = maxLives;
        UIManager.Instance.UpdateLives(currentLives); 
    }

    public void TakeDamage(float damage)
    {
        currentLives -= damage;
        UIManager.Instance.UpdateLives(currentLives);

        Debug.Log("Lives Left: " + currentLives);

        if (currentLives <= 0)
        {
            Debug.Log("Game Over!");
            UIManager.Instance.ShowGameOver(); 
            gameObject.SetActive(false); 
        }
    }
}
