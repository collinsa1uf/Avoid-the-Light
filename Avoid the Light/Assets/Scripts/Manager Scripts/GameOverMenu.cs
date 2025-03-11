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

        Cursor.visible = true; // Show cursor
    }

    public void RetryLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}