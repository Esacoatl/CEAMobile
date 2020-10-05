using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Options : MonoBehaviour
{
    public GameObject OptionsPanel;

    public string Custom_LevelEsceneName = "04_Custom";
    public string Creditos_LevelEsceneName = "02_Intro";
    bool InOptions;

    void Start()
    {
        OptionsPanel.SetActive(false);
        InOptions = false;
    }

    public void OpenOptions()
    {
        InOptions = !InOptions;
        OptionsPanel.SetActive(InOptions);        
    }

    public void Continuar()
    {
        OptionsPanel.SetActive(false);
        InOptions = false;
    }

    public void Creditos()
    {
        StartCoroutine("TimeToCreditos");
    }

    IEnumerator TimeToCreditos()
    {
        float TiempoLoad = Time.realtimeSinceStartup + .85f;
        yield return new WaitUntil(() => Time.realtimeSinceStartup > TiempoLoad);        
        SceneManager.LoadScene(Creditos_LevelEsceneName);
    }

    public void Custom()
    {
        StartCoroutine("TimeToCustom");
    }

    IEnumerator TimeToCustom()
    {
        float TiempoLoad = Time.realtimeSinceStartup + .5f;
        yield return new WaitUntil(() => Time.realtimeSinceStartup > TiempoLoad);
        SceneManager.LoadScene(Custom_LevelEsceneName);
    }

    public void Salir()
    {
        StartCoroutine("TimeToExit");
    }

    IEnumerator TimeToExit()
    {
        float TiempoLoad = Time.realtimeSinceStartup + .85f;
        yield return new WaitUntil(() => Time.realtimeSinceStartup > TiempoLoad);
        Application.Quit();
        Debug.Log("Aplication Exit Done");
    }
}
