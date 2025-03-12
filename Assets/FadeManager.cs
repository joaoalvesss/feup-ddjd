using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance; 
    private CanvasGroup fadeCanvas;
    
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

        fadeCanvas = GetComponentInChildren<CanvasGroup>();
    }

     public IEnumerator FadeOutIn(float duration, System.Action onFadeComplete)
     {
     Debug.Log("Fade Out Started");
     yield return StartCoroutine(Fade(1, duration / 2)); // Fade out
     Debug.Log("Fade Out Complete - Teleporting Player");
     onFadeComplete?.Invoke(); // Teleport the player
     Debug.Log("Player Teleported");
     yield return StartCoroutine(Fade(0, duration / 2)); // Fade in
     Debug.Log("Fade In Complete");
     }


    private IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = fadeCanvas.alpha;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }
        fadeCanvas.alpha = targetAlpha;
    }
}
