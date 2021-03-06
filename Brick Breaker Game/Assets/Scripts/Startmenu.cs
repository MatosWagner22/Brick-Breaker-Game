﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Startmenu : MonoBehaviour
{
    public GameObject LevelsPanel, Level1, Level2, Level3, Level4, Level5;
    public bool levelsPanel;
    public Text highscore_text;

    void Start()
    {
        AvailableLevels(PlayerPrefs.GetInt("HIGHSCORE"));

        if (PlayerPrefs.GetInt("HIGHSCORE") != 0)
        {
            highscore_text.text = "High Score: " + PlayerPrefs.GetInt("HIGHSCORE");
        }
    }

    public void Play()
    {
        PlayerPrefs.SetInt("CurrentLevel", 0);
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void LevelPanel()
    {
        levelsPanel = !levelsPanel;
        LevelsPanel.SetActive(levelsPanel);
    }

    void AvailableLevels(int Score)
    {
        if (Score > 17)
        {
            Level1.SetActive(true);
        }

        if (Score > 83)
        {
            Level2.SetActive(true);
        }

        if (Score > 127)
        {
            Level3.SetActive(true);
        }

        if (Score > 193)
        {
            Level4.SetActive(true);
        }

        if (Score > 249)
        {
            Level5.SetActive(true);
        }
    }
}
