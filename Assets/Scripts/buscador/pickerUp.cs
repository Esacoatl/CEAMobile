using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickerUp : MonoBehaviour
{
    public Transform thePickPlace;

    public bool btnPressed; 
    public bool isInPickZone;
    public bool isInWaterZone;
    public GameObject indicatorPick;

    public buscadorGameplay buscadorGameplay;

    private void Start()
    {
        btnPressed = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            BtnPress();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            BtnRelease();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "waterSource_Buscador")
        {
            isInWaterZone = true;
            buscadorGameplay.ToolInWater();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "pickZone")
        {
            isInPickZone = true;
            indicatorPick.SetActive(true);
        }

        if (other.tag == "pickZone" && btnPressed)
        {
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = thePickPlace.position;
            this.transform.parent = GameObject.Find("PickPlace").transform;
        } else
        {
            GetComponent<Rigidbody>().useGravity = true;
            this.transform.parent = null;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "pickZone")
        {
            indicatorPick.SetActive(false);
            isInPickZone = false;
        }
    }

    public void BtnPress ()
    {
        btnPressed = true;
    }

    public void BtnRelease()
    {
        btnPressed = false;
    }
}
