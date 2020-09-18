using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class buscadorGameplay : MonoBehaviour
{
    public float timeOfGame = 60f;
    public float timeSlider = 60f;
    public int secondsLeft = 60;
    public bool gameover = false;
    public bool winLevel = false;
    public bool takingAway = false;

    public Slider slider;
    public Text litrosScoreText;

    public GameObject gameoverScreen;
    public GameObject winScreen;
    public GameObject textDisplay;

    public GameObject itemContainer;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 20; i++)
        {
            GameObject item1 = itemContainer.transform.GetChild(Random.Range(0, itemContainer.transform.childCount)).gameObject;
            GameObject item2 = itemContainer.transform.GetChild(Random.Range(0, itemContainer.transform.childCount)).gameObject;

            Vector3 item1position = item1.transform.position;
            item1.transform.position = item2.transform.position;
            item2.transform.position = item1position;

        }
        //randomItem.transform.localScale = Vector3.one * 0.5f;
        //randomItem.tag = "CorrectItem";
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

        timeOfGame -= Time.deltaTime;
        timeSlider = timeOfGame * 0.01f;
        slider.value = secondsLeft;
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
        takingAway = true;
        winLevel = true;
        winScreen.SetActive(true);
        litrosScoreText.text = secondsLeft.ToString() + " Litros";
    }

    public void WinLoad(string nextScene)
    {

        int litrosTemp = PlayerPrefs.GetInt("litrosSum") + secondsLeft;
        PlayerPrefs.SetInt("litrosSum", litrosTemp);
        PlayerPrefs.SetString("nextSceneName", nextScene);
        StartCoroutine(WaitCoroutine());

    }

    public void WinLoadSceneMain()
    {
        SceneManager.LoadScene("MultiLoader");
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
        takingAway = true;
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
