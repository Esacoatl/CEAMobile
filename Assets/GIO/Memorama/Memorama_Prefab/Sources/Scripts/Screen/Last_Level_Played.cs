using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Last_Level_Played : MonoBehaviour
{    
    public string Actual_Module_Name = "M1_B1";

    void Start()
    {
        PlayerPrefs.SetString("Actual_Module_Played", Actual_Module_Name);
    }    
}
