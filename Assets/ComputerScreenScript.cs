using UnityEngine;

public class ComputerScreen : MonoBehaviour
{
    public GameObject zoomedInScreen; 
    private bool playerNearby = false;
    private bool isViewingScreen = false;

    private void Update()
    {
        
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !isViewingScreen)
        {
            ShowScreen();
        }
        
     
        if (isViewingScreen && Input.GetKeyDown(KeyCode.Escape))
        {
            HideScreen();
        }
    }

    private void ShowScreen()
    {
        isViewingScreen = true;
        zoomedInScreen.SetActive(true); 
        Time.timeScale = 0f; 
    }

    private void HideScreen()
    {
        isViewingScreen = false;
        zoomedInScreen.SetActive(false); 
        Time.timeScale = 1f; 
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
