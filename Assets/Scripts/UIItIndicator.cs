using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIItIndicator : MonoBehaviour
{
    public GameObject enemyAI;
    public GameObject player;
    public TextMeshProUGUI enemyItIndicator;
    public TextMeshProUGUI playerItIndicator;
    public CountDownTimer timer;
    private bool hasStarted = false; 

    void Start()
    {   
        // Make sure these don't appear during the countdown
        enemyItIndicator.enabled = false;
        playerItIndicator.enabled = false;
    }
    void Update()
    {
        // Do this once at the start
        if (!hasStarted && timer != null && timer.getTimerFinished())
        {
            enemyItIndicator.enabled = false;
            playerItIndicator.enabled = true;
            hasStarted = true;
        }
        // If it is the enemyAI that is it, then show the text for the enemy, else show the text for the player
        if(hasStarted){
            if(TagManager.Instance.IsIt(enemyAI)){
                enemyItIndicator.enabled = true;
                playerItIndicator.enabled = false;
            }
            if( TagManager.Instance.IsIt(player)){
                enemyItIndicator.enabled = false;
                playerItIndicator.enabled = true;
            }
        }
    }
}
