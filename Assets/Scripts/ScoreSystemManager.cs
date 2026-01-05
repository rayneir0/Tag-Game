using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystemManager : MonoBehaviour
{
    public static ScoreSystemManager Instance; 
    private int gameCount = 1;
    // public TextMeshProUGUI winnerText;
    public string winnerText;

    // Dictionary to store cumulative hits for the leaderboard scene
    private Dictionary<string, int> totalScore = new Dictionary<string, int>();


    // Singleton Manager so that the dictionary does not get overwritten
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize cumulative score
            totalScore["Player"] = 0;
            totalScore["Enemy"] = 0;
        }
        else Destroy(gameObject);
    }

    public void AddScore(string character, int points)
    {
        if (!totalScore.ContainsKey(character)) return;
        totalScore[character] += points; // Adds the score to the player/enemy

        Debug.Log($"{character}{totalScore[character]}");

        // If it cannot find the leaderboard
        LeaderboardScoreController leaderboard = FindObjectOfType<LeaderboardScoreController>();
        if (leaderboard != null)
            leaderboard.UpdateLeaderboardText();
        
    
    }

    public void CalculateResults()
    {

        // Find who was last tagged from the tag manager
        GameObject lastTagged = TagManager.Instance.currentIt;
        if (lastTagged == null) return;

        // Then since the player that was last tagged lost, get the name of the winner
        string winner;
        if (lastTagged.name == "Player")
            winner = "Enemy";
        else
            winner = "Player";

        AddScore(winner, 1); // Add a point for each win
        winnerText = winner;

        Debug.Log(winner);
    }

    public int GetScore(string character)
    {
        if (!totalScore.ContainsKey(character)) return 0;
        return totalScore[character];


    }

    // For the Game Timer Class when it needs to show who won the game at the end of the game
    public string GetWinner()
    {
       return winnerText;
    }
}
