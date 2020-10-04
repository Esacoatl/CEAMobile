using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSplashLoadingScreen : MonoBehaviour
{
    AudioSource Audio;
    SplashLoadingScreen Loading;

    private void Awake()
    {
        Audio = this.GetComponent<AudioSource>();
        Loading = FindObjectOfType<SplashLoadingScreen>();

        if (Loading)
        {
            Debug.Log("Objeto LoadingSplashScreen Finded");
            StartCoroutine("WaitLoaadingScreen");
        }            
        else
        {
            Debug.Log("Objeto LoadingSplashScreen Not Finded");
        }                  
    }

    IEnumerator WaitLoaadingScreen()
    {
        PlayAudio(false);
        yield return new WaitUntil(() => SplashLoadingScreen.END == true);
        PlayAudio(true);
    }

    void PlayAudio(bool Play)
    {
        if (Play)
        {
            Audio.Play();
        }
        else
        {
            Audio.Stop();
        }
    }

}
