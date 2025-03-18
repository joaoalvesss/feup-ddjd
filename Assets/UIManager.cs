using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI livesText;
    public GameObject gameOverUI;

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    public TextMeshProUGUI scoreText;
    private int currentScore = 0;

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

        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (dialogueBox != null) dialogueBox.SetActive(false);
    }

    public void UpdateLives(float lives)
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + Mathf.Ceil(lives);
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    public int GetFinalScore()
    {
        return currentScore;
    }

    public void ShowGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void RetryGame()
    {
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(ResetPlayerHealth());
    }

    private IEnumerator ResetPlayerHealth()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerHealth playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ResetHealth();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    public void ShowDialogue(string message)
    {
        Debug.Log("Showing dialogue box");
        if (dialogueBox != null && dialogueText != null)
        {
            dialogueBox.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(TypeDialogue(message));
        }
    }

    private IEnumerator TypeDialogue(string message)
    {
        dialogueText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void HideDialogue()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }
}
