using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoWebGL_Test : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    [SerializeField]
    private bool shouldPlay;

    private void Awake()
    {
        //videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Video_final.mp4");
        //videoPlayer.url = System.IO.Path.Combine("Assets/Videos/Video_final.mp4");
    }

    private void Start()
    {
        //videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        /* if (shouldPlay)
             videoPlayer.Play();
         else
             videoPlayer.Pause();
       */
    }


    private string status;
    void OnGUI()
    {
        GUIStyle buttonWidth = new GUIStyle(GUI.skin.GetStyle("button"));
        buttonWidth.fontSize = 18 * (Screen.width / 800);

        if (GUI.Button(new Rect(Screen.width / 16, Screen.height / 16, Screen.width / 3, Screen.height / 8), status, buttonWidth))
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                status = "Press to play";
            }
            else
            {
                videoPlayer.Play();
                status = "Press to pause";
            }
        }
    }

    public void reproducir (){
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            status = "Press to play";
        }
        else
        {
            videoPlayer.Play();
            status = "Press to pause";
        }
    }


}
