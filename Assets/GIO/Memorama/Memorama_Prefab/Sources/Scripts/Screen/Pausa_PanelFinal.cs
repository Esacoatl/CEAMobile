using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa_PanelFinal : MonoBehaviour
{
    Pausa ScriptPausa;

    private void Awake()
    {
        ScriptPausa = (Pausa)FindObjectOfType(typeof(Pausa));
    }

    private void OnEnable()
    {
        if (ScriptPausa)
        {
            ScriptPausa.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (ScriptPausa)
        {
            ScriptPausa.gameObject.SetActive(true);
        }
    }
}
