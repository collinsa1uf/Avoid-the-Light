using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    public void GameOver()
    {
        PauseMenu.isPaused = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RetryLevel()
    {
        PauseMenu.isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}