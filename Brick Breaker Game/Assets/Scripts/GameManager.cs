using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Lives;
    public int Score;

    public Text score_text, lives_text;

    // Start is called before the first frame update
    void Start()
    {
        lives_text.text = "Lives: " + Lives;
        score_text.text = "Score: " + Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int changesInLives)
    {
        Lives += changesInLives;

        // Check for no lifes left

        lives_text.text = "Lives: " + Lives;
    }

    public void UpdateScore(int points)
    {
        Score += points;
        score_text.text = "Score: " + Score;
    }

}
