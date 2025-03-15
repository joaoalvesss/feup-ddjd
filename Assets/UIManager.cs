using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; 
    public TextMeshProUGUI livesText; 
    public GameObject gameOverUI; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateLives(float lives)
    {
        livesText.text = "Lives: " + Mathf.Ceil(lives);
    }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true); 
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!"); 
        Application.Quit(); 
    }
}
