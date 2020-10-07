﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainGameplayController : MonoBehaviour
{
    public Text litrosScoreText;
    public static int litrosScore;
    // Start is called before the first frame update
    void Start()
    {
        litrosScore = PlayerPrefs.GetInt("litrosSum");
        string litrosText = litrosScore.ToString();
        litrosScoreText.text = "Huella Hidrica: " + litrosText + " Litros";
    }

    public void LevelLoadOption(string nextScene)
    {
        PlayerPrefs.SetString("nextSceneName", nextScene);
        WinLoadSceneMain();
    }

    public void WinLoadSceneMain()
    {
        SceneManager.LoadScene("MultiLoader");
    }

}
