using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memorama : MonoBehaviour
{
    public static int CuentaClicks;

    [HideInInspector]
    public int Count = 0;
    [HideInInspector]
    public int idA;
    [HideInInspector]
    public int idB;
    [HideInInspector]
    public Targeta TargetA;
    [HideInInspector]
    public Targeta TargetB;

    public int WinPairs;
    public PanelInfo Panel;
    public GameObject FinalPanel;

    [Header("Sonido Fin del Juego")]
    public float VolumeMax = 0.4f;
    public float VolumeDelta = 0.01f;    

    [Header("Variables del Tablero")]
    public GameObject Posiciones;
    public GameObject Tarjetas;

    Transform[] Posiciones_Vector;
    List<Transform> Targetas_Vector;
    Targeta[] Targetas_Script_Vector;

    List<int> Posiciones_Ocupadas;

    AudioSource BGMusic;
    AudioSource FinalMusic;

    int Pairs = 0;

    [HideInInspector]
    public bool FinJuego = false;
    [Header("Control Externo")]
    public bool ControlExterno_FinJuego = false;


    private void Awake()
    {
        Posiciones_Vector = Posiciones.GetComponentsInChildren<Transform>();
        Targetas_Script_Vector = Tarjetas.GetComponentsInChildren<Targeta>();
        Targetas_Vector = new List<Transform>();

        Posiciones_Ocupadas = new List<int>();

        foreach (Targeta TargetaS in Targetas_Script_Vector)
        {
            Targetas_Vector.Add(TargetaS.gameObject.GetComponent<Transform>());
        }

        foreach (Transform Targeta in Targetas_Vector)
        {
            Debug.Log(Targeta.position.x);
        }
    }

    void Start()
    {
        BGMusic = BuscarGameObject.ObtenerComponente<AudioSource>("BackGraundMusic");
        FinalMusic = FinalPanel.GetComponent<AudioSource>();
        FinalPanel.SetActive(false);

        FillPosiciones();

        int i = 0;
        foreach (Transform Targeta in Targetas_Vector)
        {           
            Targeta.position = Posiciones_Vector[Posiciones_Ocupadas[i]].position;
            i++;
        }
    }

    void FillPosiciones()
    {
        int NumElementos = Posiciones_Vector.Length - 1;
        int pos;

        while (NumElementos > 0)
        {
            pos = Random.Range(1, Posiciones_Vector.Length);
            if (!Posiciones_Ocupadas.Contains(pos))
            {
                Posiciones_Ocupadas.Add(pos);
                NumElementos--;
            }
        }
    }

    
    void Update()
    {
        if (Count == 2)
        {
            Count = 0;
            Memo();
        }                      
    }

    public void AddCount()
    {
        Count++;
    }


    void Memo()
    {
        Memorama.CuentaClicks = 0;

        if (idA == idB)
        {
            Pairs++;
            Panel.Result(true);
            Debug.Log(Pairs);
            TargetA.Matched = true;
            TargetB.Matched = true;
            EmptyTarget();

            if (Pairs == WinPairs)
            {
                if (ControlExterno_FinJuego)
                {
                    FinJuego = true;
                }
                else
                {
                    StartCoroutine("EsperarFin");
                }                                
            }
        }
        else
        {
            Panel.Result(false);
            StartCoroutine("EsperarFlip_Back");      
        }        
    }

    IEnumerator EsperarFin()
    {
        yield return new WaitUntil(() => Panel.Activo == true);
        yield return new WaitUntil(() => Panel.Activo == false);
        BGMusic.Pause();
        BGMusic.volume = 0;
        FinalPanel.SetActive(true);
        StartCoroutine("WaitMusicEnds");
    }

    IEnumerator EsperarFlip_Back()
    {
        yield return new WaitUntil(() => Panel.Activo == true);
        yield return new WaitUntil(() => Panel.Activo == false);
        TargetA.Flip_Back();
        TargetB.Flip_Back();        
        EmptyTarget();
    }

    void EmptyTarget()
    {
        TargetA = null;
        TargetB = null;
        idA = 0;
        idB = 0;        
    }

    IEnumerator WaitMusicEnds()
    {
        yield return new WaitUntil(() => FinalMusic.isPlaying == false);
        BGMusic.Play();
        StartCoroutine(AudioLerp());
    }

    IEnumerator AudioLerp()
    {
        while (BGMusic.volume <= VolumeMax)
        {            
            BGMusic.volume += VolumeDelta;
            yield return new WaitForEndOfFrame();
        }
    }
}
