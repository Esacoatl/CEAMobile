using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Final : MonoBehaviour
{
    public VideoPlayer VideoPlay;    
    public string Credits_LevelEsceneName = "02_Intro";
    
    bool VideoFlag;

    [Header("Boton WebGL Play")]
    public GameObject Boton_WebGL_Play;

    private void Awake()
    {
        if (Boton_WebGL_Play)
        {
            Boton_WebGL_Play.SetActive(false);
        }
    }

    private void Start()
    {
        VideoPlay.Stop();
        VideoPlay.frame = 0;
        VideoPlay.gameObject.SetActive(false);
        VideoPlay.gameObject.SetActive(true);
        if (Boton_WebGL_Play)
        {
            Boton_WebGL_Play.SetActive(true);
        }
        StartCoroutine("Wait_Deactive_Video");
    }

    private void Update()
    {
        if ((System.Convert.ToInt64(VideoPlay.frameCount) <= (VideoPlay.frame + 2)) && !VideoFlag)
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
        SceneManager.LoadScene(Credits_LevelEsceneName);
    }
}
