using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameController : MonoBehaviour
{
    public CountDownTimer countdownTimer;
    public GameTimer gameTimer;
    public GameObject gameTimerObj;
    public GameObject playerItIndicator;
    public GameObject enemyItIndicator;
    private bool hasStartedGame = false;


    void Update()
    {
        // Restart the GameTimer, and put back the it indicator texts
        if (countdownTimer.getTimerFinished() && !hasStartedGame)
        {
            hasStartedGame = true;
            gameTimer.StartTimer();
            gameTimerObj.SetActive(true);
            playerItIndicator.SetActive(true);
            enemyItIndicator.SetActive(true);
        }
    }

    // Restart the Countdown Timer
    public void RestartGame()
    {
        hasStartedGame = false;
        RemoveUI();
        gameTimer.ResetTimer();     
        countdownTimer.ResetTimer();
        countdownTimer.StartTimer();
    }

    public void RemoveUI()
    {
        gameTimerObj.SetActive(false);
        playerItIndicator.SetActive(false);
        enemyItIndicator.SetActive(false);
    }

}
