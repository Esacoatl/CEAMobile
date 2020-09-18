using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWorldValues : MonoBehaviour
{
    public Text litrosScoreText;
    // score general de litros ahorrados
    public static int litrosScore;
    // nombre de nueva Scena a cargar
    public static string sceneNameStr;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("litrosSum"))
        {
            litrosScore = PlayerPrefs.GetInt("litrosSum");
        }
        else
        {
            litrosScore = 0;
            PlayerPrefs.SetInt("litrosSum", litrosScore);
        }
        string litrosText = litrosScore.ToString();
        litrosScoreText.text = litrosText + " Litros";
    }

    public void SetNombreScene(string NewScene)
    {
        sceneNameStr = NewScene;
        PlayerPrefs.SetString("nextSceneName", NewScene);
    }

    public void MasLitrosScore(int newLitros)
    {
        litrosScore = litrosScore + newLitros;
    }

    /*public void ResetLitrosScore() {
        PlayerPrefs.DeleteKey("litrosSum");
        PlayerPrefs.DeleteAll();
    }*/
}
