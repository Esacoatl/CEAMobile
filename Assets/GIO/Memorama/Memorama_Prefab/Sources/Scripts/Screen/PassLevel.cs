using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassLevel : MonoBehaviour
{       
    string Actual_LevelEsceneName;
    string Next_LevelEsceneName;

    int Next_LevelEsceneBuildIndex;
    string StatusLevel;    

    Scene ActualScene;
    Scene NextScene;

    void Awake()
    {
        ActualScene = SceneManager.GetActiveScene();        
        Actual_LevelEsceneName = ActualScene.name;
        Debug.Log("Actual Scene Name: " + Actual_LevelEsceneName);

        Next_LevelEsceneBuildIndex = ActualScene.buildIndex + 1;
        Debug.Log("Next Buildt Index: " + Next_LevelEsceneBuildIndex);

        //NextScene = SceneManager.GetSceneByBuildIndex(6);
        //Next_LevelEsceneName = NextScene.path;

        Next_LevelEsceneName = GetSceneNameFromScenePath(SceneUtility.GetScenePathByBuildIndex(Next_LevelEsceneBuildIndex));        
        Debug.Log("Next Scene Name: " + Next_LevelEsceneName);
    }

    void Start()
    {
        StatusLevel = PlayerPrefs.GetString(Actual_LevelEsceneName, "ToPlay");

        if (StatusLevel == "ToPlay")
        {
            PlayerPrefs.SetString(Actual_LevelEsceneName, "Completo");
            PlayerPrefs.SetString(Next_LevelEsceneName, "ToPlay");

            PlayerPrefs.SetString("Last_Level_Played", Actual_LevelEsceneName);

            StatusLevel = PlayerPrefs.GetString(Actual_LevelEsceneName, "ToPlay");
            Debug.Log("EEEE  " + StatusLevel);
        }        
    }

    private string GetSceneNameFromScenePath(string scenePath)
    {
        // Unity's asset paths always use '/' as a path separator
        var sceneNameStart = scenePath.LastIndexOf("/", StringComparison.Ordinal) + 1;
        var sceneNameEnd = scenePath.LastIndexOf(".", StringComparison.Ordinal);
        var sceneNameLength = sceneNameEnd - sceneNameStart;
        return scenePath.Substring(sceneNameStart, sceneNameLength);
    }
}
