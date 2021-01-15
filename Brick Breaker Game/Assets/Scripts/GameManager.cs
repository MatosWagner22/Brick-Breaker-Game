﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Lives;
    public int Score;
    public int numberOfBricks;

    public Transform Explosion;
    public Text score_text, lives_text, score_final, highscore_text;
    public GameObject gameOverPanel, loadLevelPanel;
    public Transform[] Levels;
    public int currentLevelIndex = 0;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        lives_text.text = "Lives: " + Lives;
        score_text.text = "Score: " + Score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int changesInLives)
    {
        Lives += changesInLives;

        if (Lives <= 0)
        {
            Lives = 0;
            GameOver();
        }

        lives_text.text = "Lives: " + Lives;
    }

    public void UpdateScore(int points)
    {
        Score += points;
        score_text.text = "Score: " + Score;
    }

    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)
        {
            if (currentLevelIndex >= Levels.Length - 1)
            {
                GameOver();
            }
            else
            {
                loadLevelPanel.SetActive(true);
                Lives++;
                loadLevelPanel.GetComponentInChildren<Text>().text = "Loading Level " + (currentLevelIndex + 2);
                gameOver = true;
                Invoke("Loadlevel", 3f);
            }
        }
    }

    void Loadlevel()
    {
        currentLevelIndex++;
        Instantiate(Levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        lives_text.text = "Lives: " + Lives;
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        score_final.text = "Score: " + Score;
        if (Score > highScore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", Score);
            highscore_text.text = "New High Score: " + highScore;
            Transform newExplosion = Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(newExplosion.gameObject, 2.5f);
        }
        else
        {
            highscore_text.text = "High Score: " + highScore;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
