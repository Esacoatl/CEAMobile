using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;
using UnityEngine.UI;

public class WorldMap_Manager : MonoBehaviour
{
    [Header("Last Level Played Names")]
    const string Last_Level_PLayed_M1_Bx = "07_M1_B3_E04";
    const string Last_Level_PLayed_M2_B1 = "07_M2_B1_E03";
    const string Last_Level_PLayed_M2_B2 = "07_M2_B2_E03";
    const string Last_Level_PLayed_M2_B3 = "07_M2_B3_E03";
    const string Last_Level_PLayed_M3_B1 = "07_M3_B1_E04";
    const string Last_Level_PLayed_M3_B2 = "07_M3_B2_E02";

    [Header("Actual Module Played Names")]
    const string Actual_Module_PLayed_M1_Bx = "M1_B1";
    const string Actual_Module_PLayed_M2_B1 = "M2_B1";
    const string Actual_Module_PLayed_M2_B2 = "M2_B2";
    const string Actual_Module_PLayed_M2_B3 = "M2_B3";
    const string Actual_Module_PLayed_M3_B1 = "M3_B1";
    const string Actual_Module_PLayed_M3_B2 = "M3_B2";

    [Header("Soldados")]
    public Animator Soldados;
    public Animator SoldadoH;
    public Animator SoldadoM;

    [Header("Posiciones Soltado")]
    public Transform Posicion_M1_Bx;
    public Transform Posicion_M2_B1;
    public Transform Posicion_M2_B2;
    public Transform Posicion_M2_B3;
    public Transform Posicion_M3_B1;
    public Transform Posicion_M3_B2;    

    [Header("Cameras")]
    public MasterDialogo_E03 Camera1;
    public Transform Camera2;
    public GameObject Camera3;

    [Header("Posiciones Camera 2")]
    public Transform Camera2_M1_Bx;
    public Transform Camera2_M2_B1;
    public Transform Camera2_M2_B2;
    public Transform Camera2_M2_B3;
    public Transform Camera2_M3_B1;
    public Transform Camera2_M3_B2;

    [Header("Canvas Dialogos")]
    public GameObject Canvas1;
    public GameObject Canvas2;
    public GameObject Canvas3;

    [Header("Dialogos Personages")]
    public GameObject Dialog_Personaje_1;
    public GameObject Dialog_Personaje_2;
    public GameObject Dialog_Personaje_3;

    [Header("Pagination Scripts")]
    public UIPagination Dialog_Personaje_Pagination_script_1;
    public UIPagination Dialog_Personaje_Pagination_script_2;
    public UIPagination Dialog_Personaje_Pagination_script_3;

    [Header("Dialogos Content Pages")]
    public GameObject Content_Dialog1;
    public GameObject Content_Dialog2;
    public GameObject Content_Dialog3;

    [Header("Block Objects")]
    public GameObject BlockObject1;
    public GameObject BlockObject2;
    public GameObject BlockObject3;

    [Header("UI Button Objects")]
    public GameObject PausaButton;
    public GameObject CustomButton;
    public GameObject M1Button;
    public GameObject M2Button;
    public GameObject M3Button;
    public GameObject M4Button;
    public GameObject M5Button;
    public GameObject M6Button;

    [Header("Back Graund Music")]
    public AudioSource BackGraundMusic;
    public AudioClip ClipIntro;

    [Header("Text Name")]
    public Text TextName;

    [Header("Medallas Animaciones")]
    public MasterDialogo_E03 Bronce_script;
    public GameObject Insignia_script;

    [Header("Medallas")]
    public GameObject Bronce;
    public GameObject Plata;
    public GameObject Oro;
    public GameObject Insignia;

    [Header("Banderas Animaciones")]
    public MasterDialogo_E03 Bandera_1_script;
    public MasterDialogo_E03 Bandera_2_script;

    [Header("Banderas")]
    public GameObject Bandera_1;
    public GameObject Bandera_2;

    MasterDialogo_E03 Dialog_Personaje_script_1;
    Animator Dialog_Personaje_animator_1;

    MasterDialogo_E03 Dialog_Personaje_script_2;
    Animator Dialog_Personaje_animator_2;

    MasterDialogo_E03 Dialog_Personaje_script_3;
    Animator Dialog_Personaje_animator_3;

    MasterDialogo_E03 Soldados_script;

    AudioSource[] PagesAudios1;
    AudioSource[] PagesAudios2;
    AudioSource[] PagesAudios3;

    string Last_Level_PLayed;
    string Actual_Module_Played;

    string Evento0;
    string Evento1;
    string Evento2;
    string Evento3;
    string Evento4;
    string Evento5;
    string Evento6;

    private void Awake()
    {
        Dialog_Personaje_script_1 = Dialog_Personaje_1.GetComponent<MasterDialogo_E03>();
        Dialog_Personaje_animator_1 = Dialog_Personaje_1.GetComponent<Animator>();

        Dialog_Personaje_script_2 = Dialog_Personaje_2.GetComponent<MasterDialogo_E03>();
        Dialog_Personaje_animator_2 = Dialog_Personaje_2.GetComponent<Animator>();

        Dialog_Personaje_script_3 = Dialog_Personaje_3.GetComponent<MasterDialogo_E03>();
        Dialog_Personaje_animator_3 = Dialog_Personaje_3.GetComponent<Animator>();

        Soldados_script = Soldados.GetComponent<MasterDialogo_E03>();

        PagesAudios1 = Content_Dialog1.GetComponentsInChildren<AudioSource>();
        PagesAudios2 = Content_Dialog2.GetComponentsInChildren<AudioSource>();
        PagesAudios3 = Content_Dialog3.GetComponentsInChildren<AudioSource>();

        Load();
    }

    void Load()
    {
        Last_Level_PLayed = PlayerPrefs.GetString("Last_Level_Played", "");
        Actual_Module_Played = PlayerPrefs.GetString("Actual_Module_Played", Actual_Module_PLayed_M1_Bx);

        TextName.text = PlayerPrefs.GetString("Nombre", "");

        Evento0 = PlayerPrefs.GetString("Evento0", "");
        Evento1 = PlayerPrefs.GetString("Evento1", "");
        Evento2 = PlayerPrefs.GetString("Evento2", "");
        Evento3 = PlayerPrefs.GetString("Evento3", "");
        Evento4 = PlayerPrefs.GetString("Evento4", "");
        Evento5 = PlayerPrefs.GetString("Evento5", "");
        Evento6 = PlayerPrefs.GetString("Evento6", "");
    }

    void Start()
    {
        Inicia();
        PosicionaSoldado();
        SetEventos();
    }

    void Inicia()
    {
        Canvas1.SetActive(false);
        Canvas2.SetActive(false);
        Canvas3.SetActive(false);
        
        Camera1.gameObject.SetActive(false);
        Camera3.gameObject.SetActive(false);
    }

    void PosicionaSoldado()
    {
        switch (Actual_Module_Played)
        {
            case Actual_Module_PLayed_M1_Bx:
                Soldados.SetTrigger("Pos1");
                Camera2.position = Camera2_M1_Bx.position;
                break;
            case Actual_Module_PLayed_M2_B1:
                Soldados.SetTrigger("Pos2");
                Camera2.position = Camera2_M2_B1.position;
                break;
            case Actual_Module_PLayed_M2_B2:
                Soldados.SetTrigger("Pos3");
                Camera2.position = Camera2_M2_B2.position;
                break;
            case Actual_Module_PLayed_M2_B3:                
                if (Last_Level_PLayed == Last_Level_PLayed_M2_B3)
                {
                    Soldados.SetTrigger("Pos5");
                    Camera2.position = Camera2_M3_B1.position;
                }
                else
                {                    
                    Soldados.SetTrigger("Pos4");
                    Camera2.position = Camera2_M2_B3.position;
                }
                break;
            case Actual_Module_PLayed_M3_B1:
                if(Last_Level_PLayed == Last_Level_PLayed_M3_B1)
                {
                    Soldados.SetTrigger("Pos6");
                    Camera2.position = Camera2_M3_B2.position;
                }
                else
                {
                    Soldados.SetTrigger("Pos5");
                    Camera2.position = Camera2_M3_B1.position;
                }               
                break;
            case Actual_Module_PLayed_M3_B2:
                Soldados.SetTrigger("Pos6");
                Camera2.position = Camera2_M3_B2.position;
                break;
        }
    }

    void SetUIElements(bool Active)
    {
        PausaButton.SetActive(Active);
        CustomButton.SetActive(Active);
        M1Button.SetActive(Active);
        M2Button.SetActive(Active);
        M3Button.SetActive(Active);
        M4Button.SetActive(Active);
        M5Button.SetActive(Active);
        M6Button.SetActive(Active);
        
        TextName.gameObject.SetActive(Active);
    }

    void SetEventos()
    {
        if (Evento0 != "false")
        {
            PlayerPrefs.SetString("Evento0", "false");
            Evento0 = "false";            
            Ejecuta_Evento0();
        }
        else if (Last_Level_PLayed == Last_Level_PLayed_M1_Bx && Evento1 != "false")
        {
            PlayerPrefs.SetString("Evento1", "false");
            Evento1 = "false";            
            Ejecuta_Evento1();
        }
        else if (Last_Level_PLayed == Last_Level_PLayed_M2_B1 && Evento2 != "false")
        {
            PlayerPrefs.SetString("Evento2", "false");
            Evento2 = "false";            
            Ejecuta_Evento2();
        }
        else if (Last_Level_PLayed == Last_Level_PLayed_M2_B2 && Evento3 != "false")
        {
            PlayerPrefs.SetString("Evento3", "false");
            Evento3 = "false";            
            Ejecuta_Evento3();
        }
        else if (Last_Level_PLayed == Last_Level_PLayed_M2_B3 && Evento4 != "false")
        {
            PlayerPrefs.SetString("Evento4", "false");
            Evento4 = "false";            
            Ejecuta_Evento4();
        }
        else if (Last_Level_PLayed == Last_Level_PLayed_M3_B1 && Evento5 != "false")
        {
            PlayerPrefs.SetString("Evento5", "false");
            Evento5 = "false";            
            Ejecuta_Evento5();
        }
        else if (Last_Level_PLayed == Last_Level_PLayed_M3_B2 && Evento6 != "false")
        {
            PlayerPrefs.SetString("Evento6", "false");
            Evento6 = "false";            
            Ejecuta_Evento6();
        }
    }

    void Update()
    {
        if (Dialog_Personaje_Pagination_script_1.End == true)
        {
            Dialog_Personaje_Pagination_script_1.End = false;
            Dialog_Personaje_animator_1.SetTrigger("DeactivePanel");
            StartCoroutine("Wait_Deactive_Dialog_Personaje_1");
        }
        else if (Dialog_Personaje_Pagination_script_2.End == true)
        {
            Dialog_Personaje_Pagination_script_2.End = false;
            Dialog_Personaje_animator_2.SetTrigger("DeactivePanel");
            StartCoroutine("Wait_Deactive_Dialog_Personaje_2");
        }
        else if (Dialog_Personaje_Pagination_script_3.End == true)
        {
            Dialog_Personaje_Pagination_script_3.End = false;
            Dialog_Personaje_animator_3.SetTrigger("DeactivePanel");
            StartCoroutine("Wait_Deactive_Dialog_Personaje_3");
        }
    }

    /*____________________________________________________________________________________________________*/
    /*____________________________________________________________________________________________________*/
    void Ejecuta_Evento0()
    {
        Debug.Log("EVENTO: 0");
        Camera2.gameObject.SetActive(false);
        Camera1.gameObject.SetActive(true);

        BlockObject1.SetActive(true);
        BlockObject2.SetActive(true);
        BlockObject3.SetActive(true);

        BackGraundMusic.Stop();
        BackGraundMusic.clip = ClipIntro;
        BackGraundMusic.Play();

        StartCoroutine("Wait_Time_To_Deactive_BlockObject");
        StartCoroutine("Wait_Deactive_Camera3");
    }

    IEnumerator Wait_Time_To_Deactive_BlockObject()
    {
        yield return new WaitForSeconds(0.01f);
        BlockObject1.SetActive(false);
        BlockObject2.SetActive(false);
        BlockObject3.SetActive(false);
        SetUIElements(false);
    }

    IEnumerator Wait_Deactive_Camera3()
    {
        yield return new WaitUntil(() => Camera1.DeactivePanel_Ended == true);
        BlockObject2.SetActive(true);
        BlockObject3.SetActive(true);
        Canvas1.SetActive(true);
        Dialog_Personaje_1.SetActive(true);        
        StartCoroutine("Wait_Deactive_Dialog_Personaje_1_Audios");
    }

    IEnumerator Wait_Deactive_Dialog_Personaje_1_Audios()
    {
        yield return new WaitUntil(() => PagesAudios1[Dialog_Personaje_Pagination_script_1.activePage].isPlaying == false);
        Dialog_Personaje_Pagination_script_1.OnNextClick();
        StartCoroutine("Wait_Deactive_Dialog_Personaje_1_Audios");
    }

    IEnumerator Wait_Deactive_Dialog_Personaje_1()
    {
        yield return new WaitUntil(() => Dialog_Personaje_script_1.DeactivePanel_Ended == true);
        StopCoroutine("Wait_Deactive_Dialog_Personaje_1_Audios");
        Camera2.gameObject.SetActive(true);
        Camera2.position = Camera2_M1_Bx.position;
        Camera1.gameObject.SetActive(false);
        SetUIElements(true);
    }

    /*____________________________________________________________________________________________________*/
    /*____________________________________________________________________________________________________*/
    void Ejecuta_Evento1()
    {
        Debug.Log("EVENTO: 1");
        StartCoroutine("Wait_Time_To_TimeOne_Bronce");

    }

    IEnumerator Wait_Time_To_TimeOne_Bronce()
    {
        yield return new WaitUntil(() => Time.timeScale == 1);
        Camera2.gameObject.SetActive(false);
        Camera3.gameObject.SetActive(true);        
        Bronce.SetActive(false);
        Bronce_script.gameObject.SetActive(true);
        StartCoroutine("Wait_Deactive_Bronce");        
    }

    IEnumerator Wait_Deactive_Bronce()
    {
        yield return new WaitUntil(() => Bronce_script.DeactivePanel_Ended == true);
        yield return new WaitForSeconds(1.15f);
        Soldados.SetBool("DesplazaColegio", true);
        SoldadoH.SetBool("Camina", true);
        SoldadoM.SetBool("Camina", true);

        StartCoroutine("Wait_Time_To_Deactive_UIElements");
        StartCoroutine("Wait_Deactive_Soldados");
    }

    IEnumerator Wait_Time_To_Deactive_UIElements()
    {
        yield return new WaitForEndOfFrame();      
        SetUIElements(false);
    }

    IEnumerator Wait_Deactive_Soldados()
    {
        yield return new WaitUntil(() => Soldados_script.DeactivePanel_Ended == true);

        Soldados.SetBool("DesplazaColegio", false);
        SoldadoH.SetBool("Camina", false);
        SoldadoM.SetBool("Camina", false);

        Canvas2.SetActive(true);
        Dialog_Personaje_2.SetActive(true);        
        //StartCoroutine("Wait_Deactive_Dialog_Personaje_2_Audios");
    }

    IEnumerator Wait_Deactive_Dialog_Personaje_2_Audios()
    {
        yield return new WaitUntil(() => PagesAudios2[Dialog_Personaje_Pagination_script_2.activePage].isPlaying == false);
        Dialog_Personaje_Pagination_script_2.OnNextClick();
        StartCoroutine("Wait_Deactive_Dialog_Personaje_2_Audios");
    }

    IEnumerator Wait_Deactive_Dialog_Personaje_2()
    {
        yield return new WaitUntil(() => Dialog_Personaje_script_2.DeactivePanel_Ended == true);
        Dialog_Personaje_2.SetActive(false);
        //StopCoroutine("Wait_Deactive_Dialog_Personaje_2_Audios");
        Camera2.gameObject.SetActive(true);
        Camera2.position = Camera2_M2_B1.position;
        Camera3.gameObject.SetActive(false);
        SetUIElements(true);

        Bronce.SetActive(true);
        Bronce_script.gameObject.SetActive(false);
    }

    /*____________________________________________________________________________________________________*/
    /*____________________________________________________________________________________________________*/
    void Ejecuta_Evento2()
    {
        Debug.Log("EVENTO: 2");
        Soldados.SetTrigger("Pos2");
        Camera2.position = Camera2_M2_B1.position;
        StartCoroutine("Wait_Time_To_TimeOne_Bandera_1");        
    }

    IEnumerator Wait_Time_To_TimeOne_Bandera_1()
    {
        yield return new WaitUntil(() => Time.timeScale == 1);
        Camera2.gameObject.SetActive(false);
        Camera3.gameObject.SetActive(true);        

        Bandera_1.SetActive(false);
        Bandera_1_script.gameObject.SetActive(true);

        StartCoroutine("Wait_Deactive_Bandera_1");
    }

    IEnumerator Wait_Deactive_Bandera_1()
    {
        yield return new WaitUntil(() => Bandera_1_script.DeactivePanel_Ended == true);
        yield return new WaitForSeconds(1.15f);

        Soldados.SetBool("DesplazaGuerrero", true);
        SoldadoH.SetBool("Camina", true);
        SoldadoM.SetBool("Camina", true);

        StartCoroutine("Wait_Time_To_Deactive_UIElements");
        StartCoroutine("Wait_Deactive_Soldados2");
    }

        IEnumerator Wait_Deactive_Soldados2()
    {
        yield return new WaitUntil(() => Soldados_script.DeactivePanel_Ended == true);

        Soldados.SetBool("DesplazaGuerrero", false);
        SoldadoH.SetBool("Camina", false);
        SoldadoM.SetBool("Camina", false);

        Canvas3.SetActive(true);
        Dialog_Personaje_3.SetActive(true);
        //StartCoroutine("Wait_Deactive_Dialog_Personaje_3_Audios");
    }

    IEnumerator Wait_Deactive_Dialog_Personaje_3_Audios()
    {
        yield return new WaitUntil(() => PagesAudios3[Dialog_Personaje_Pagination_script_3.activePage].isPlaying == false);
        Dialog_Personaje_Pagination_script_3.OnNextClick();
        StartCoroutine("Wait_Deactive_Dialog_Personaje_3_Audios");
    }

    IEnumerator Wait_Deactive_Dialog_Personaje_3()
    {
        yield return new WaitUntil(() => Dialog_Personaje_script_3.DeactivePanel_Ended == true);
        Dialog_Personaje_3.SetActive(false);
        //StopCoroutine("Wait_Deactive_Dialog_Personaje_3_Audios");
        Camera2.gameObject.SetActive(true);
        Camera2.position = Camera2_M2_B2.position;
        Camera3.gameObject.SetActive(false);
        SetUIElements(true);

        Bandera_1.SetActive(true);
        Bandera_1_script.gameObject.SetActive(false);
    }

    /*____________________________________________________________________________________________________*/
    /*____________________________________________________________________________________________________*/

    void Ejecuta_Evento3()
    {
        Debug.Log("EVENTO: 3");
        Soldados.SetTrigger("Pos3");
        Camera2.position = Camera2_M2_B2.position;
        StartCoroutine("Wait_Time_To_TimeOne_Bandera_2");               
    }

    IEnumerator Wait_Time_To_TimeOne_Bandera_2()
    {
        yield return new WaitUntil(() => Time.timeScale == 1);
        Camera2.gameObject.SetActive(false);
        Camera3.gameObject.SetActive(true);

        Bandera_2.SetActive(false);
        Bandera_2_script.gameObject.SetActive(true);

        StartCoroutine("Wait_Deactive_Bandera_2");
    }

    IEnumerator Wait_Deactive_Bandera_2()
    {
        yield return new WaitUntil(() => Bandera_2_script.DeactivePanel_Ended == true);
        yield return new WaitForSeconds(1.15f);

        Soldados.SetBool("DesplazaRancho", true);
        SoldadoH.SetBool("Camina", true);
        SoldadoM.SetBool("Camina", true);

        StartCoroutine("Wait_Time_To_Deactive_UIElements");
        StartCoroutine("Wait_Deactive_Soldados3");
    }

    IEnumerator Wait_Deactive_Soldados3()
    {
        yield return new WaitUntil(() => Soldados_script.DeactivePanel_Ended == true);

        Soldados.SetBool("DesplazaRancho", false);
        SoldadoH.SetBool("Camina", false);
        SoldadoM.SetBool("Camina", false);

        Camera2.gameObject.SetActive(true);
        Camera2.position = Camera2_M2_B3.position;
        Camera3.gameObject.SetActive(false);
        SetUIElements(true);

        Bandera_2.SetActive(true);
        Bandera_2_script.gameObject.SetActive(false);
    }

    /*____________________________________________________________________________________________________*/
    /*____________________________________________________________________________________________________*/
    void Ejecuta_Evento4()
    {
        Debug.Log("EVENTO: 4");
    }

    /*____________________________________________________________________________________________________*/
    /*____________________________________________________________________________________________________*/
    void Ejecuta_Evento5()
    {
        Debug.Log("EVENTO: 5");
    }

    /*____________________________________________________________________________________________________*/
    /*____________________________________________________________________________________________________*/
    void Ejecuta_Evento6()
    {
        Debug.Log("EVENTO: 6");
        Insignia_script.SetActive(true);
        StartCoroutine("Wait_Time_To_Deactive_Insignia");
    }

    IEnumerator Wait_Time_To_Deactive_Insignia()
    {
        yield return new WaitForSeconds(0.01f);
        Insignia.SetActive(false);
    }

}
