using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLevel : MonoBehaviour
{
    [Header("Levels Names")]
    public string LevelName;

    [Header("Select Visibility")]
    public GameObject Open;
    public GameObject Close;
    public GameObject Medalla;

    string StatusLevel;

    private void Awake()
    {
        if(LevelName == "07_M1_B1_E01")
        {
            StatusLevel = PlayerPrefs.GetString(LevelName, "ToPlay");
            Debug.Log("EEEE  " + StatusLevel);
        }
        else
        {
            StatusLevel = PlayerPrefs.GetString(LevelName, "Block");
        }

        switch (StatusLevel)
        {
            case "Completo":
                Open.SetActive(true);
                Close.SetActive(false);
                Medalla.SetActive(true);
                break;
            case "ToPlay":
                Open.SetActive(true);
                Close.SetActive(false);
                Medalla.SetActive(false);
                break;
            case "Block":
                Open.SetActive(false);
                Close.SetActive(true);
                Medalla.SetActive(false);
                break;
        }
    }

    void Start()
    {
        
    }
}
