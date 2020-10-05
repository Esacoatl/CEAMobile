using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Inicio : MonoBehaviour
{
    public VideoPlayer VideoPlay;
    public GameObject Canvas;
    public string WordlMap_LevelEsceneName = "05_WorldMap";
    public string Custom_LevelEsceneName = "04_Custom";
    
    string Primero;
    bool VideoFlag;

    [Header("Boton WebGL Play")]
    public GameObject Boton_WebGL_Play;

    private void Awake()
    {
        Primero = PlayerPrefs.GetString("Primero", "");

        if (Boton_WebGL_Play)
        {
            Boton_WebGL_Play.SetActive(false);
        }
    }

    private void Start()
    {
        VideoPlay.Stop();
        VideoPlay.frame = 0;

        if (Primero != "false")
        {
            VideoPlay.gameObject.SetActive(false);
            VideoPlay.gameObject.SetActive(true);
            if (Boton_WebGL_Play)
            {
                Boton_WebGL_Play.SetActive(true);
            }
            StartCoroutine("Wait_Deactive_Video");
            Canvas.SetActive(false);
        }
        else
        {
            VideoPlay.gameObject.SetActive(false);
            Canvas.SetActive(true);
        }
    }

    public void CargaEscenaInicio()
    {
        if (Primero == "false")
        {
            SceneManager.LoadScene(WordlMap_LevelEsceneName);
        }
        else
        {
            SceneManager.LoadScene(Custom_LevelEsceneName);
        }
    }

    private void Update()
    {
        if ((System.Convert.ToInt64(VideoPlay.frameCount) <= (VideoPlay.frame + 2)) && !VideoFlag && Primero != "false")
        {
            if (!Boton_WebGL_Play)
            {
                Video_Off();
            }
        }
    }

    IEnumerator Wait_Deactive_Video()
    {
        yield return new WaitUntil(() => VideoPlay.isPlaying == true);
        yield return new WaitUntil(() => VideoPlay.isPlaying == false);

        if (!VideoFlag)
        {
            Video_Off();
        }
    }

    void Video_Off()
    {
        VideoFlag = true;
        SceneManager.LoadScene(Custom_LevelEsceneName);
    }

}
