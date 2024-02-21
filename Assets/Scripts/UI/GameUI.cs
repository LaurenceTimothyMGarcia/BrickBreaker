using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private PlayerScore pScore;

    [Header("Primary Game UI")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMP_Text scoreText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOver;

    void Start()
    {
        pScore.ClearScore();
        pScore.gameOver = false;
        gameUI.SetActive(true);
        gameOver.SetActive(false);
    }

    void Update()
    {
        UpdateScore();

        if (pScore.gameOver)
        {
            GameOver();
            pScore.gameOver = false;
        }
    }

    public void UpdateScore()
    {
        scoreText.text = pScore.currentScore.ToString();
    }

    public void GameOver()
    {
        gameUI.SetActive(false);
        gameOver.SetActive(true);
        pScore.CheckHighestScore();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void TitleMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
