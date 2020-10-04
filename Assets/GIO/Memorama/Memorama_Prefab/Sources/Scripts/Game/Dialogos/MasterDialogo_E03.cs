using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDialogo_E03 : MonoBehaviour
{

    [HideInInspector]
    public bool ActivePanel_Ended = false;
    [HideInInspector]
    public bool DeactivePanel_Ended = false;

    [HideInInspector]
    public bool ActivePanel_Started = false;
    [HideInInspector]
    public bool DeactivePanel_Started = false;

    public void Start_PanelActivado()
    {
        ActivePanel_Ended = false;
        DeactivePanel_Ended = false;

        ActivePanel_Started = true;
        DeactivePanel_Started = false;
    }

    public void Start_PanelDeactivado()
    {
        DeactivePanel_Ended = false;
        ActivePanel_Ended = false;

        ActivePanel_Started = false;
        DeactivePanel_Started = true;
    }

    public void End_PanelActivado()
    {
        ActivePanel_Ended = true;
        DeactivePanel_Ended = false;

        ActivePanel_Started = false;
        DeactivePanel_Started = false;
    }

    public void End_PanelDeactivado()
    {
        DeactivePanel_Ended = true;
        ActivePanel_Ended = false;

        ActivePanel_Started = false;
        DeactivePanel_Started = false;
    }

}
