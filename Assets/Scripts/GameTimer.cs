using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{

    // References
    public TextMeshProUGUI timerText;
    public CountDownTimer timer;
    public GameObject gameOverMenu;
    public PauseMenuController pauseMenu;
    public TextMeshProUGUI winnerText;

    // Timer variables
    public float startTime;
    private float timeRemaining;
    private bool hasStarted = false;

    void Start()
    {
        gameOverMenu.SetActive(false);
    }

    void Update()
    {
        // Checks if the timer hasn't started and the countdown timer is finished
        if (!hasStarted && timer != null && timer.getTimerFinished())
        {
            // Start after the countdowntimer
            StartTimer();
            hasStarted = true;
        }

        // If the timer is not zero, decrease the time
        if (hasStarted && timeRemaining > 0)
        {

            // Subtracts the timeRemaining based on computer's clock
            timeRemaining -= Time.deltaTime;

            // Change the format for more anxiety -> Add milisceconds
            if (timeRemaining > 60)
            {
                FormatTime();
            }
            else
            {
                timerText.text = timeRemaining.ToString("0.00");
            }
        }

        // If the timer has reached zero
        if (hasStarted && timeRemaining <= 0 && !gameOverMenu.activeSelf)
        {
            // Clamp to zero otherwise it will show a negative number
            timeRemaining = 0;
            // Show the Game over menu
            gameOverMenu.SetActive(true);

            // Calculate who won
            if (ScoreSystemManager.Instance == null) return;
            ScoreSystemManager.Instance.CalculateResults();

            // Get the winner and show who won to the player in the menu
            string winner = ScoreSystemManager.Instance.GetWinner();

            if (winnerText != null)
            {
                winnerText.text = $"Winner: {winner}";
            }

            // Pause the game so it doesn't move
            pauseMenu.PauseGame();
            hasStarted = false;
        }
    }

    public void StartTimer()
    {   
        timeRemaining = startTime;
        timerText.gameObject.SetActive(true);
    }

    void FormatTime()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetTimer()
    {
        hasStarted = false;
    }

}
