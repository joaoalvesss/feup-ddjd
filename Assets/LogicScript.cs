using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public GameObject gameOver;

    [ContextMenu("Add Score")]
    public void AddScore(int num)
    {
        score += num;
        scoreText.text = score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver.SetActive(false);
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }
}
