using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Pausa : MonoBehaviour
{
    public GameObject PausePanel; 

    public string WordlMap_LevelEsceneName = "05_WorldMap";

    string Actual_LevelEsceneName;
    bool Pausado;

    VideoPlayer VideoInScene;
    //AudioSource[] AudiosInScene;

    private void Awake()
    {
        Actual_LevelEsceneName = SceneManager.GetActiveScene().name;
        VideoInScene = (VideoPlayer)FindObjectOfType(typeof(VideoPlayer));
        //AudiosInScene = FindObjectsOfType<AudioSource>();
    }

    void Start()
    {
        PausePanel.SetActive(false);
        Pausado = false;
    }

    public void PausaJuego()
    {
        Pausado = !Pausado;
        PausePanel.SetActive(Pausado);
        if (Pausado)
        {
            Time.timeScale = 0f;
            if (VideoInScene)
            {
                VideoInScene.playbackSpeed = 0f;
            }            
        }        
        else
        {
            Time.timeScale = 1f;
            if (VideoInScene)
            {
                VideoInScene.playbackSpeed = 1f;
            }           
        }
    }

    public void Continuar()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        Pausado = false;

        if (VideoInScene)
        {
            VideoInScene.playbackSpeed = 1f;
        }        
    }

    public void Recargar()
    {                
        StartCoroutine("TimeToReload");
    }

    IEnumerator TimeToReload()
    {
        float TiempoLoad = Time.realtimeSinceStartup + .85f;
        yield return new WaitUntil(() => Time.realtimeSinceStartup > TiempoLoad);
        Time.timeScale = 1f;
        SceneManager.LoadScene(Actual_LevelEsceneName);
    }

    public void Salir()
    {        
        StartCoroutine("TimeToExit");
    }

    IEnumerator TimeToExit()
    {
        float TiempoLoad = Time.realtimeSinceStartup + .85f;
        yield return new WaitUntil(() => Time.realtimeSinceStartup > TiempoLoad);
        Time.timeScale = 1f;
        SplashLoadingScreen.LoadScene(WordlMap_LevelEsceneName);
    }

}
