using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Lives;
    public int Score;
    public int numberOfBricks;

    public Text score_text, lives_text, score_final, highscore_text, score_pause;
    public GameObject gameOverPanel, loadLevelPanel, pausePanel;
    public Transform[] Levels;
    public int loadingLevel = 0, currentLevelIndex = 0;

    private GameObject UnbreakableBricks;
    public bool gameOver, pause;

    // Start is called before the first frame update
    void Start()
    {
        lives_text.text = "Lives: " + Lives;
        score_text.text = "Score: " + Score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        UnbreakableBricks = GameObject.FindGameObjectWithTag("Unbreakable");

        LevelSelect(PlayerPrefs.GetInt("CurrentLevel"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            KeepPlay();
        }
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
                LevelLoading();
            }
        }
    }

    void LevelLoading()
    {
        loadLevelPanel.SetActive(true);
        loadLevelPanel.GetComponentInChildren<Text>().text = "Loading Level " + (loadingLevel + 1);
        gameOver = true;
        Invoke("Loadlevel", 3f);
    }

    void LevelSelect(int level)
    {
        /* Level 1 = 18             18
         * Level 2 = 66             84
         * Level 3 = 44             128
         * Level 4 = 66             194
         * Level 5 = 56             250
        */
        if (level == 0)
        {
            loadingLevel = level;
            Score = 0;
        }

        if (level == 1)
        {
            loadingLevel = level;
            Score = 18;
        }

        if (level == 2)
        {
            loadingLevel = level;
            Score = 84;
        }

        if (level == 3)
        {
            loadingLevel = level;
            Score = 128;
        }

        if (level == 4)
        {
            loadingLevel = level;
            Score = 194;
        }

        score_text.text = "Score: " + Score;
        LevelLoading();
    }

    void Loadlevel()
    {
        loadingLevel++;
        currentLevelIndex = loadingLevel - 1;
        Destroy(UnbreakableBricks);
        Instantiate(Levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        Lives++;
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
            highscore_text.text = "New High Score: " + Score;
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

    public void Home()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadScene("StartMenu");
    }

    public void Pausar()
    {
        if (Time.timeScale == 1)
        {
            score_pause.text = "Score: " + Score;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void KeepPlay()
    {
        Pausar();
        pause = !pause;
        pausePanel.SetActive(pause);
    }

}
