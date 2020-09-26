using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class hueyaHidrInGame : MonoBehaviour
{
    public Text litrosScoreTextInGame;
    // score general de litros ahorrados
    public static int litrosScoreInGame;
    // Start is called before the first frame update
    void Start()
    {
        litrosScoreInGame = PlayerPrefs.GetInt("litrosSum");
        string litrosText = litrosScoreInGame.ToString();
        litrosScoreTextInGame.text = "Hueya Hidrica: " + litrosText + " Litros";
    }
}
