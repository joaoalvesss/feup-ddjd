using UnityEngine;

public class ComputerScreen : MonoBehaviour
{
    public GameObject zoomedInScreen; // Assign the UI Image in Inspector
    private bool playerNearby = false;
    private bool isViewingScreen = false;

    private void Update()
    {
        // Player presses E to open screen
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !isViewingScreen)
        {
            ShowScreen();
        }
        
        // Player presses Esc to close screen
        if (isViewingScreen && Input.GetKeyDown(KeyCode.Escape))
        {
            HideScreen();
        }
    }

    private void ShowScreen()
    {
        isViewingScreen = true;
        zoomedInScreen.SetActive(true); // Show zoomed-in image
        Time.timeScale = 0f; // Pause the game
    }

    private void HideScreen()
    {
        isViewingScreen = false;
        zoomedInScreen.SetActive(false); // Hide zoomed-in image
        Time.timeScale = 1f; // Resume game
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
