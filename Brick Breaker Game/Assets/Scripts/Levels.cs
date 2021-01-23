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
        //p = GameObject.FindGameObjectWithTag("GameManager");
        //gm.FindObjectOfType<GameManager>().FindPlayer(p);
    }

    public void SelectLevel()
    {
        Debug.Log(indexLevel);
       // SceneManager.LoadScene("Main");
    }
}
