using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buscadorGameplay : MonoBehaviour
{
    //public float timeOfGame = 60f;
    public int secondsLeft = 60;
    public bool gameover = false;
    public bool winLevel = false;
    public bool takingAway = false;

    public GameObject gameoverScreen;
    public GameObject winScreen;
    public GameObject textDisplay;
    public GameObject[] objectsLife = new GameObject[4];
    public GameObject[] objectsEnemys = new GameObject[3];


    // Start is called before the first frame update
    void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if(takingAway == false && secondsLeft >= 0)
        {
            StartCoroutine(TimerTake());
        } 

        if (secondsLeft <= 0)
        {
            gameover = true;
            gameoverScreen.SetActive(true);
        }
        /*timeOfGame -= Time.deltaTime;
        if (timeOfGame < 0)
        {

        }*/
    }

    IEnumerator TimerTake ()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if(secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
            textDisplay.GetComponent<Text>().color = Color.red;
            takingAway = false;
        } else 
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
            takingAway = false;
        }
    }

    // win
    public void ToolInWater()
    {
        winLevel = true;
        winScreen.SetActive(true);
    }

    public void WinLoad()
    {
        StartCoroutine(WaitCoroutine());
    }

    public void WinLoadSceneMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator WaitCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
        WinLoadSceneMain();
    }

    // gameover

    public void GameOverLoad()
    {
        StartCoroutine(WaitCoroutineGameover());
    }

    public void GameOverLoadSceneMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator WaitCoroutineGameover()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
        GameOverLoadSceneMain();
    }
}
