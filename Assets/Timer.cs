using UnityEngine;
using UnityEngine.UI; 

public class Timer : MonoBehaviour
{
    public Text timerText; 
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime; 

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
