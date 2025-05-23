using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PasswordPrompt : MonoBehaviour
{
    public GameObject passwordPanel;
    public GameObject winScreen;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI finalScoreText;
    
    public string correctPassword = "I<3minix";
    private bool playerInRange = false;

    public static bool isPasswordPanelOpen = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.W))
        {
            OpenPasswordPanel();
        }
        if (passwordPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePasswordPanel();
        }
    }

    private void OpenPasswordPanel()
    {
        passwordPanel.SetActive(true);
        passwordInput.text = "";
        warningText.gameObject.SetActive(false);
        isPasswordPanelOpen = true;
    }

    public void CheckPassword()
    {
        if (passwordInput.text == correctPassword)
        {
            Debug.Log("Password Correct! Unlocking...");
            passwordPanel.SetActive(false);
            ShowWinScreen();
        }
        else
        {
            Debug.Log("Incorrect Password. Try Again.");
            warningText.gameObject.SetActive(true);
        }
    }

    private void ShowWinScreen()
    {
        winScreen.SetActive(true);
        isPasswordPanelOpen = true;

        int finalScore = UIManager.Instance.GetFinalScore();
        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + finalScore;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void ClosePasswordPanel()
    {
        passwordPanel.SetActive(false);
        warningText.gameObject.SetActive(false);
        isPasswordPanelOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player is near. Press W to enter password.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            ClosePasswordPanel();
        }
    }
}
