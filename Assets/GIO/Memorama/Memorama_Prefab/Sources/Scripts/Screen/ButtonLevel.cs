using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    [Header("Levels Names")]
    public string LevelBlockName;
    public string Last_LevelEsceneName;
    public ButtonLevel Next_LevelBlockName;
    public bool Final = false;

    [Header("Image Icon")]
    public Image Image_Icon;

    [Header("Sprites Icon")]
    public Sprite Sprite_Icon_Completo;
    public Sprite Sprite_Icon_ToPlay;
    public Sprite Sprite_Icon_Block;

    [Header("Level Menu")]
    public GameObject LevelMenu;

    [Header("Audio Clips")]
    public AudioClip ClipOpen;
    public AudioClip ClipBlock;

    [Header("Block Object")]
    public GameObject BlockObject;

    [Header("Final Truck")]
    public GameObject FinalTruck;

    [Header("Medallas")]
    public GameObject M1;

    [Header("Banderas")]
    public GameObject B1;

    [Header("Manita")]
    public GameObject Manita;

    AudioSource Audio;

    [HideInInspector]
    string BlockStatus;
    string LastLevel;

    private void Awake()
    {
        Audio = this.GetComponent<AudioSource>();

        if(LevelBlockName == "M1_B1")
        {
            BlockStatus = PlayerPrefs.GetString(LevelBlockName, "ToPlay");
        }
        else
        {
            BlockStatus = PlayerPrefs.GetString(LevelBlockName, "Block");
        }      
        
        LastLevel = PlayerPrefs.GetString(Last_LevelEsceneName, "Block");

        if (LastLevel == "Completo")
        {
            BlockStatus = "Completo";
            PlayerPrefs.SetString(LevelBlockName, "Completo");            
        }
    }

    void Start()
    {
        LevelMenu.SetActive(false);
        Seleciona();              
    }

    public void Seleciona()
    {
        switch (BlockStatus)
        {
            case "Completo":
                Image_Icon.sprite = Sprite_Icon_Completo;
                Audio.clip = ClipOpen;

                if (!Final)
                {
                    if (Next_LevelBlockName.BlockStatus == "Block")
                    {
                        Next_LevelBlockName.Desbloquea();
                    }
                }

                if (BlockObject)
                {
                    BlockObject.SetActive(false);
                }

                if (FinalTruck)
                {
                    FinalTruck.SetActive(true);
                }

                if (M1)
                {
                    M1.SetActive(true);
                }
                if (B1)
                {
                    B1.SetActive(true);
                }

                Manita.SetActive(false);
                break;
            case "ToPlay":
                Image_Icon.sprite = Sprite_Icon_ToPlay;
                Audio.clip = ClipOpen;

                if (BlockObject)
                {
                    BlockObject.SetActive(false);
                }

                if (FinalTruck)
                {
                    FinalTruck.SetActive(true);
                }

                if (M1)
                {
                    M1.SetActive(false);
                }
                if (B1)
                {
                    B1.SetActive(false);
                }

                Manita.SetActive(true);

                break;
            case "Block":
                Image_Icon.sprite = Sprite_Icon_Block;
                Audio.clip = ClipBlock;

                if (BlockObject)
                {
                    BlockObject.SetActive(true);
                }

                if (FinalTruck)
                {
                    FinalTruck.SetActive(false);
                }

                if (M1)
                {
                    M1.SetActive(false);
                }
                if (B1)
                {
                    B1.SetActive(false);
                }

                Manita.SetActive(false);
                break;
        }
    }

    public void Action_Level()
    {
        Audio.Play();
        switch (BlockStatus)
        {
            case "Completo":
                LevelMenu.SetActive(true);
                break;
            case "ToPlay":
                LevelMenu.SetActive(true);
                break;
        }
    }

    public void Close_LevelMenu()
    {
        LevelMenu.SetActive(false);
    }

    public void Desbloquea()
    {
        PlayerPrefs.SetString(LevelBlockName, "ToPlay");
        BlockStatus = "ToPlay";        
        Seleciona();
    }
        
    void Update()
    {
        
    }
}
