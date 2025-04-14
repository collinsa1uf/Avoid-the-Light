using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameManager gameManager;
    public GameObject player;
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

    public void RetryFromCheckpoint()
    {
        gameManager.Respawn(player);
        player.GetComponent<DraculaController>().ResetPlayer();
        PauseMenu.isPaused = false;
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public void RetryLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}