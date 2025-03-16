using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;
    public GameObject leaderboardPanel;
    public GameObject howToPlayPanel;

    private void Start()
    {
        ShowMainMenu(); // Show main menu at the start
    }

    private void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }

    public void ShowLevelSelect()
    {
        HideAllPanels();
        levelSelectPanel.SetActive(true);
    }

    public void ShowLeaderboard()
    {
        HideAllPanels();
        leaderboardPanel.SetActive(true);
    }

    public void ShowHowToPlay()
    {
        HideAllPanels();
        howToPlayPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
        Debug.Log("Loading Level: " + levelName);
        SceneManager.LoadScene(levelName);
    }
}
