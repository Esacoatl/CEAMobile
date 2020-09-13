using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickerUp : MonoBehaviour
{
    public Transform thePickPlace;

    public bool btnPressed; 
    public bool isInPickZone;

    private void Start()
    {
        btnPressed = false;
    }
    private void Update()
    {

    }
    public void OnTriggerStay(Collider other)
    {
        //&& btnPressed
        if (other.tag == "pickZone" && btnPressed)
        {
            isInPickZone = true;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = thePickPlace.position;
            this.transform.parent = GameObject.Find("PickPlace").transform;
        } else
        {
            isInPickZone = false;
            GetComponent<Rigidbody>().useGravity = true;
            this.transform.parent = null;
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

    /*public void PickeUpAction()
    {
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = thePickPlace.position;
        this.transform.parent = GameObject.Find("PickPlace").transform;
    }

    public void PickeUpDown()
    {
        GetComponent<Rigidbody>().useGravity = true;
        this.transform.parent = null;
    }*/
}
