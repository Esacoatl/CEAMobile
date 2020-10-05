using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfo : MonoBehaviour
{
    public GameObject InfoPanel;
    public GameObject TextPanel;
    public GameObject ImagePanel;

    public GameObject CorrectPanel;
    public GameObject IncorrectPanel;

    public Text Title;
        
    public Image Image;
    public TextMeshProUGUI Text2;

    public TextMeshProUGUI Text;
    public Image Image2;

    [HideInInspector]
    public bool Is_Image;

    [HideInInspector]
    public bool Activo = false;

    [HideInInspector]
    public Targeta TargetA;
    [HideInInspector]
    public Targeta TargetB;

    [HideInInspector]
    public Targeta TargetAA;
    [HideInInspector]
    public Targeta TargetBB;

    bool one_touch = false;

    Animator InfoPanelAnimator;
    AudioSource InfoPanelAudio;

    

    private void Awake()
    {
        InfoPanelAnimator = GetComponent<Animator>();
        InfoPanelAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        InfoPanel.SetActive(false);
        Text2.gameObject.SetActive(false);
        Image2.gameObject.SetActive(false);        
    }

    
    void Update()
    {
        if (Activo)
        {
            if (one_touch && (Input.GetButtonDown("Fire1") || Input.touchCount > 0))
            {
                one_touch = false;
                HidePanel();
            }
            
        }
        
    }

    public void ShowPanel()
    {
        InfoPanel.SetActive(true);
        TextPanel.SetActive(false);
        ImagePanel.SetActive(false);
        InfoPanelAnimator.SetTrigger("ShowPanel");
    }

    public void HidePanel()
    {
        InfoPanelAnimator.SetTrigger("HidePanel");
        InfoPanelAudio.Play();        
    }

    public void EnablePanel()
    {
        Activo = true;
        one_touch = true;
    }

    public void DesablePanel()
    {
        InfoPanel.SetActive(false);
        Activo = false;
        Text2.gameObject.SetActive(false);
        Image2.gameObject.SetActive(false);
        CorrectPanel.SetActive(false);
        IncorrectPanel.SetActive(false);
    }

    public void SelectType()
    {
        if (Is_Image)
        {
            TextPanel.SetActive(true);
            ImagePanel.SetActive(false);
        }
        else
        {
            TextPanel.SetActive(false);
            ImagePanel.SetActive(true);
        }
    }

    public void Result(bool Correct)
    {
        ShowPanel();
        SetTargets();

        if (Correct)
        {
            CorrectPanel.SetActive(true);
            IncorrectPanel.SetActive(false);
        }
        else
        {
            CorrectPanel.SetActive(false);
            IncorrectPanel.SetActive(true);
        }

    }

    void SetTargets()
    {
        Title.text = TargetA.Title;        

        if (TargetA.Is_Image)
        {
            Image.sprite = TargetA.Image.sprite;            
        }
        else
        {
            Image.sprite = null;
            Text2.gameObject.SetActive(true);
            Text2.text = TargetA.Text.text;
        }

        if (TargetB.Is_Image)
        {
            Text.text = "";
            Image2.gameObject.SetActive(true);
            Image2.sprite = TargetB.Image.sprite;            
        }
        else
        {            
            Text.text = TargetB.Text.text;
        }

        TextPanel.SetActive(false);
        ImagePanel.SetActive(false);
    }

    public void ResultFinal()
    {
        ShowPanel();
        SetTargetsFinal();
       
        CorrectPanel.SetActive(true);
        IncorrectPanel.SetActive(false);        

    }

    void SetTargetsFinal()
    {
        Title.text = TargetAA.Title;

        if (TargetAA.Is_Image)
        {
            Image.sprite = TargetAA.Image.sprite;
        }
        else
        {
            Image.sprite = null;
            Text2.gameObject.SetActive(true);
            Text2.text = TargetAA.Text.text;
        }

        if (TargetBB.Is_Image)
        {
            Text.text = "";
            Image2.gameObject.SetActive(true);
            Image2.sprite = TargetBB.Image.sprite;
        }
        else
        {
            Text.text = TargetBB.Text.text;
        }

        TextPanel.SetActive(false);
        ImagePanel.SetActive(false);
    }
}
