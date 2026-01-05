using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardScoreController : MonoBehaviour
{

    // Two different texts to use to accumulate points
    public TextMeshProUGUI leaderboardPlayerText;
    public TextMeshProUGUI leaderboardEnemyText;
    void Start()
    {
        UpdateLeaderboardText();
    }

    public void UpdateLeaderboardText()
    {
        if (ScoreSystemManager.Instance == null) return;

        // Get the score from the score manager
        int playerScore = ScoreSystemManager.Instance.GetScore("Player");
        int enemyScore = ScoreSystemManager.Instance.GetScore("Enemy");

        Debug.Log($"Player Score: {ScoreSystemManager.Instance.GetScore("Player")}");
        Debug.Log($"Enemy Score: {ScoreSystemManager.Instance.GetScore("Enemy")}");

        // Convert them to string to update the text on the screen
        if (leaderboardPlayerText != null)
            leaderboardPlayerText.text = playerScore.ToString();

        if (leaderboardEnemyText != null)
            leaderboardEnemyText.text = enemyScore.ToString();
    }
}
