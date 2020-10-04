using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Targeta : MonoBehaviour
{
    public int idPair = 1;
    public string Title;
    public bool Is_Image;    
    public Image Image;
    public TextMeshProUGUI Text;

    public Canvas Canvas_Back;
    public Canvas Canvas_Front;

    [HideInInspector]
    public bool Matched = false;

    Animator FlipAnimator;
    
    PanelInfo Panel;
    Memorama Memo;

    bool one_click = true;

    private void Awake()
    {
        FlipAnimator = GetComponent<Animator>();
        Panel = BuscarGameObject.ObtenerComponente<PanelInfo>("Info");
        Memo = BuscarGameObject.ObtenerComponente<Memorama>("GameManager");
    }


    void Start()
    {
        Canvas_Back.gameObject.SetActive(true);
        Canvas_Front.gameObject.SetActive(false);

        if (Is_Image)
        {
            Text.gameObject.SetActive(false);
            Image.gameObject.SetActive(true);
        }
        else
        {
            Text.gameObject.SetActive(true);
            Image.gameObject.SetActive(false);
        }
    }

    
    void Update()
    {
        
    }


    public void Flip()
    {
        if (one_click && Memorama.CuentaClicks <= 1)
        {
            one_click = false;
            Memorama.CuentaClicks++;

            one_click = false;
            Canvas_Back.gameObject.SetActive(true);
            Canvas_Front.gameObject.SetActive(true);

            FlipAnimator.SetTrigger("Flip");            
        }
    }

    public void SetMemo()
    {
        Canvas_Back.gameObject.SetActive(false);
        Canvas_Front.gameObject.SetActive(true);        

        Memo.Count++;

        if (Memo.Count == 1)
        {
            Memo.idA = idPair;
            Memo.TargetA = this;
            Panel.TargetA = this;
        }
        else if (Memo.Count == 2)
        {
            Memo.idB = idPair;
            Memo.TargetB = this;
            Panel.TargetB = this;
        }
    }

    public void Flip_Back()
    {        
        Canvas_Back.gameObject.SetActive(true);
        Canvas_Front.gameObject.SetActive(true);

        FlipAnimator.SetTrigger("Flip_Back");
    }

    public void EndFlip_Back()
    {
        Canvas_Back.gameObject.SetActive(true);
        Canvas_Front.gameObject.SetActive(false);
        one_click = true;
    }

    public void FillData()
    {
        if(Matched == false)
        {
            Panel.Title.text = Title;
            Panel.Is_Image = Is_Image;

            if (Is_Image)
            {
                Panel.Image.sprite = Image.sprite;
            }
            else
            {
                Panel.Text.text = Text.text;
            }

            Panel.SelectType();
        }
        else
        {
            Panel.TargetAA = this;
            Targeta NewTargetaB = new Targeta();            
            Panel.TargetBB = NewTargetaB;

            if (Is_Image)
            {
                Panel.TargetBB.Is_Image = false;
                Panel.TargetBB.Text = Text;
            }
            else
            {
                Panel.TargetBB.Is_Image = true;
                Panel.TargetBB.Image = Image;                
            }

            Panel.ResultFinal();
        }
    }
}
