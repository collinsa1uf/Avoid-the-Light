using UnityEngine;
using UnityEngine.SceneManagement;

// Script for holding the on button click functions.
// Implemented so far are:
// ToMainMenu(): For Credits scene and pause screen, returns the player to the main menu.
// ToGameScene(): For Main Menu scene, takes player to the Gameplay scene when clicked- might change this to level select if we do that?
// ToCreditsScene(): For Main Menu scene, takes player to the credits scene when clicked.
// GameQuit(): For Main Menu scene and pause screen, quits the game when clicked.
public class ButtonManager : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void ToGameScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ToCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GameQuit()
    {
        Application.Quit();
    }
}