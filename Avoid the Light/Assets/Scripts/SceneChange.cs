using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{   public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        Time.timeScale = 1f;
        Cursor.visible = true;
    }
}