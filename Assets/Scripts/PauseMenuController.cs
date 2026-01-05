using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private static bool isPaused = false;

    void Start()
    {
        // Make sure the pause menu doesn't appear on the starting screne
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f; // Game runs at regular time
    }

    void Update()
    {
        // Checks for input
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!isPaused)
            {
                pauseMenu.SetActive(true);
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {   
        // Changes the time scale from a default of 1 to 0, basically slowing down the game until nothing moves.
        isPaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void ResumeGame()
    {
        // Returning the game to it's inital state
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1; 
        
        // Cursor disappears again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool GetPausedState()
    {
        return isPaused;
    }

}
