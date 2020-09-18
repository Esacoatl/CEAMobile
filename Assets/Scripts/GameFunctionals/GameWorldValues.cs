using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldValues : MonoBehaviour
{
    // score general de litros ahorrados
    public static int litrosScore;
    // nombre de nueva Scena a cargar
    public static string sceneNameStr;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SetNombreScene(string NewScene)
    {
        sceneNameStr = NewScene;
    }

    public void MasLitrosScore( int newLitros)
    {
        litrosScore = litrosScore + newLitros;
    }
}
