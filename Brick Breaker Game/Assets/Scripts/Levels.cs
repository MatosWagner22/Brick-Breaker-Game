using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public int indexLevel;
    //public GameManager gm;
    public GameObject p, level;

    void Start()
    {
        
    }

    public void SelectLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", indexLevel - 1);
        Debug.Log(indexLevel);
        SceneManager.LoadScene("Main");
    }
}
