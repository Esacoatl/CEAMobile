using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        load();

        Debug.Log(Helper.Serialize<SaveState>(state));
    }

    // salvar juego en player pref
    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    // Load Function de playerPref
    public void load()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("Creando Archivo de Guardado");
        }
    }

}
