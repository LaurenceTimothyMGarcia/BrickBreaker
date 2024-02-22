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

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseUI;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOver;

    void Start()
    {
        pScore.ClearScore();
        pScore.gameOver = false;
        
        LevelStart();
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

    public void LevelStart()
    {
        Time.timeScale = 1f;
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
        gameOver.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameUI.SetActive(false);
        pauseUI.SetActive(true);
        gameOver.SetActive(false);
    }

    public void GameOver()
    {
        gameUI.SetActive(false);
        pauseUI.SetActive(false);
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
