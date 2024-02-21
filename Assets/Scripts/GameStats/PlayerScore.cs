using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats")]
public class PlayerScore : ScriptableObject
{
    public int currentScore = 0;
    public int blockWorth = 100;

    public int highestScore = 0;

    public bool gameOver = false;

    public void AddScore()
    {
        currentScore += blockWorth;
    }

    public void CheckHighestScore()
    {
        if (currentScore > highestScore)
        {
            highestScore = currentScore;
        }
    }

    public void ClearScore()
    {
        currentScore = 0;
    }
}
