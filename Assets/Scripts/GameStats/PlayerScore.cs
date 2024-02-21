using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats")]
public class PlayerScore : ScriptableObject
{
    public int currentScore = 0;
    public int blockWorth = 100;

    public int[] highestScore = new int[10];

    public bool gameOver = false;

    public void AddScore(int scoreMult)
    {
        currentScore += (blockWorth * scoreMult);
    }

    public void CheckHighestScore()
    {
        for (int i = 0; i < highestScore.Length; i++)
        {
            if (currentScore > highestScore[i])
            {
                // Shift the existing scores down to make room for the new score
                for (int j = highestScore.Length - 1; j > i; j--)
                {
                    highestScore[j] = highestScore[j - 1];
                }
                highestScore[i] = currentScore;
                break; // Break out of the loop once the new score is inserted
            }
        }
    }

    public void ClearScore()
    {
        currentScore = 0;
    }

    public string ScoreText()
    {
        string scoreboard = "";

        for (int i = 0; i < highestScore.Length; i++)
        {
            // Convert score to string with leading zeros and max of 7 digits
            string formattedScore = highestScore[i].ToString("D7");
            // Print the formatted score
            scoreboard += (i + 1) + ". " + formattedScore + "\n";
        }

        return scoreboard;
    }
}
