using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;
using UnityEngine.PlayerLoop;

public class DialogoBackGraundMusic : MonoBehaviour
{
    [Header("Dialogos Content Pages")]
    public GameObject Content_Dialog;

    public bool Video = false;    

    GameObject[] Objetos_BackGraundMusic;

    AudioSource[] PagesAudios;
    List<AudioSource> AudiosBack = new List<AudioSource>();
    List<float> OriginalVolumes = new List<float>();    

    public float VolumeDelta = 0.01f;
    public float MinimunVolume = 0.08f;

    int ActualAudio = 0;

    UIPagination Pagination;

    [Header("Only UpAudio")]
    public bool OnlyLerp_Up = false;

    private void Awake()
    {        
        if (Content_Dialog)
        {
            PagesAudios = Content_Dialog.GetComponentsInChildren<AudioSource>();
        }
        else
        {
            PagesAudios = this.GetComponentsInChildren<AudioSource>();
        }        

        Pagination = GetComponentInChildren<UIPagination>();

        Objetos_BackGraundMusic = GameObject.FindGameObjectsWithTag("BackGraundMusic");
        foreach(GameObject Back in Objetos_BackGraundMusic)
        {            
            AudioSource Audio = Back.GetComponent<AudioSource>();            
            AudiosBack.Add(Audio);            
            OriginalVolumes.Add(Audio.volume);            
        }
    }

    private void OnEnable()
    {
        if (!OnlyLerp_Up)
        {
            ActualAudio = 0;
            if (PagesAudios.Length > 0)
            {
                StopAllCoroutines();
            
                if (!Video)
                {
                   foreach (AudioSource BGMusic in AudiosBack)
                    {
                        StartCoroutine(AudioLerp_Down(BGMusic));
                    }
                    if (Pagination)
                    {
                        StartCoroutine("Wait_Deactive_Pagination");
                    }
                    else
                    {
                        StartCoroutine("Wait_Deactive_Page_Audios");
                    }
                }
                else
                {
                    StartCoroutine("Wait_Play_Video");
                }
            }
        }

        else
        {
            int i = 0;
            foreach (AudioSource BGMusic in AudiosBack)
            {
                StartCoroutine(AudioLerp_Up(BGMusic, i));
                i++;
            }
        }
    }

    IEnumerator Wait_Play_Video()
    {
        yield return new WaitUntil(() => PagesAudios[ActualAudio].isPlaying == true);
        foreach (AudioSource BGMusic in AudiosBack)
        {
            StartCoroutine(AudioLerp_Down(BGMusic));
        }
        if (Pagination)
        {
            StartCoroutine("Wait_Deactive_Pagination");
        }
        else
        {
            StartCoroutine("Wait_Deactive_Page_Audios");
        }
    }

    IEnumerator AudioLerp_Down(AudioSource BGMusic)
    {
        //foreach (AudioSource BGMusic in AudiosBack)
        //{            
            while (BGMusic.volume > MinimunVolume)
            {
                BGMusic.volume -= VolumeDelta;
                yield return new WaitForEndOfFrame();
            }
        //}
    }

    IEnumerator Wait_Deactive_Pagination()
    {        
        yield return new WaitUntil(() => Pagination.End2 == true);
        Pagination.End2 = false;

        int i = 0;
        foreach (AudioSource BGMusic in AudiosBack)
        {
            StartCoroutine(AudioLerp_Up(BGMusic, i));
            i++;
        }
    }

    IEnumerator Wait_Deactive_Page_Audios()
    {        
        yield return new WaitUntil(() => PagesAudios[ActualAudio].isPlaying == false);        
        ActualAudio++;
        if(ActualAudio == PagesAudios.Length)
        {            
            StopCoroutine("AudioLerp_Down");

            int i = 0;
            foreach (AudioSource BGMusic in AudiosBack)
            {
                StartCoroutine(AudioLerp_Up(BGMusic, i));
                i++;
            }

            StopCoroutine("Wait_Deactive_Page_Audios");
        }
        else
        {
            StartCoroutine("Wait_Deactive_Page_Audios");
        }        
    }  

    IEnumerator AudioLerp_Up(AudioSource BGMusic, int i)
    {
        //int i = 0;
        //foreach (AudioSource BGMusic in AudiosBack)
        //{
            while (BGMusic.volume <= OriginalVolumes[i])
            {
                BGMusic.volume += VolumeDelta;
                yield return new WaitForEndOfFrame();
            }
            //i++;
        //}
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
