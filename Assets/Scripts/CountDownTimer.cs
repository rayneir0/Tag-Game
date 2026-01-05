using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float startTime;
    private float timeRemaining;
    private bool hasTimerFinished = false;
    private Coroutine timerCoroutine;
    
    // Reference to enemy movement
    public MonoBehaviour enemyMovement;

    void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerCoroutine = StartCoroutine(StartCountdown());
    }

    
    // Need an IEnumerator to handle the timing between the words Start and Go
    private IEnumerator StartCountdown()
    {
        // Freeze Enemy before starting the game
        if (enemyMovement != null) enemyMovement.enabled = false;
        timerText.gameObject.SetActive(true);

        // Show "Start" first
        timerText.text = "Start";
        yield return new WaitForSeconds(1f);

        timeRemaining = startTime;

        // Countdown from startTime to 1
        while (timeRemaining > 0)
        {
            timerText.text = Mathf.Ceil(timeRemaining).ToString("0");
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }

        // Show "Go"  at the end
        timerText.text = "Go!";
        yield return new WaitForSeconds(1f);

         if (enemyMovement != null) enemyMovement.enabled = true;

        // Hide the text and mark as finished
        timerText.gameObject.SetActive(false);
        hasTimerFinished = true;
        timerCoroutine = null;
    }

    public bool getTimerFinished()
    {
        return hasTimerFinished;
    }

    // Stop the coroutine and reset all the bools and the inital times
    public void ResetTimer()
    {
        // Reset the coroutine
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        timeRemaining = startTime;
        hasTimerFinished = false;

        // Freeze movement for next countdown
        if (enemyMovement != null) enemyMovement.enabled = false;


    }

}
