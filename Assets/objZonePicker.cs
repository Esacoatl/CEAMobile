using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objZonePicker : MonoBehaviour
{
    public bool EnteredTrigger;
    public GameObject CollisionWith;


    // Update is called once per frame
    void Update()
    {
        
    }

    public shooterGameplay shooterGameplay;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "drill" || other.tag == "screw")
        {
            EnteredTrigger = true;
            CollisionWith = other.gameObject;

        }
    }
}
