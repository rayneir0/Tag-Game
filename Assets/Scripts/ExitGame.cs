using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Called on the Exit button in Main Menu
    public void QuitGame()
    {
        Debug.Log("Quit button pressed!");
        Application.Quit();
    }
}
