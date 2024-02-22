using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private PlayerScore pScore;

    [Header("Main Game UI")]
    [SerializeField] private GameObject mainUI;

    [Header("Level Select UI")]
    [SerializeField] private GameObject lvlSelectUI;

    [Header("Scoreboard UI")]
    [SerializeField] private GameObject scoreboard;
    [SerializeField] private TMP_Text sbText;

    void Start()
    {
        sbText.text = pScore.ScoreText();

        MainTitleScreen();

    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainTitleScreen()
    {
        mainUI.SetActive(true);
        lvlSelectUI.SetActive(false);
        scoreboard.SetActive(false);
    }

    public void LvlSelectScreen()
    {
        mainUI.SetActive(false);
        lvlSelectUI.SetActive(true);
        scoreboard.SetActive(false);
    }

    public void ScoreboardScreen()
    {
        mainUI.SetActive(false);
        lvlSelectUI.SetActive(false);
        scoreboard.SetActive(true);
    }
}
