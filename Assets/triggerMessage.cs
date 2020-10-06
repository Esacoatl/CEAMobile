using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerMessage : MonoBehaviour
{
    public string animationIn = "InChuveje";
    public string animationOut = "OutChuveje";
    public GameObject mensajeCanvas;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider obj)
    {
        Debug.Log(obj.tag);
        if (obj.tag == "PlayerMessArea")
        {
            mensajeCanvas.SetActive(true);
            DOTween.Restart(animationIn);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerMessArea")
        {
            DOTween.Restart(animationOut);
        }
    }
}
