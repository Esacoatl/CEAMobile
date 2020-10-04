using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoWebGL_Play : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string VideoName;    

    private string status;

    private void OnEnable()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, VideoName);
    }

    public void reproducir()
    {
        if (videoPlayer.isPlaying)
        {
            //videoPlayer.Pause();
            //status = "Press to play";
        }
        else
        {
            videoPlayer.Play();
            status = "Press to pause";
            this.gameObject.SetActive(false);
        }
    }

}
