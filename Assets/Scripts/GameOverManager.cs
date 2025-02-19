using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign your GameOverPanel in the Inspector

    void Start()
    {
        // Hide the game over panel at the start
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        // Show the game over panel
        gameOverPanel.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Restart the game (reload current scene)
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        // Go to the main menu scene
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your scene name
    }
}
