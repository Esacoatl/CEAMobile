using Ludiq.PeekCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class waterTrigger : MonoBehaviour
{
    public shooterGameplay shooterGameplay;
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag.Equals("Enemy"))
        {
            //Acciones a realizar cuando se detecta una entrada al Trigger.
            shooterGameplay.BichoToWater();
            StartCoroutine(destroybicho(obj));
            
        }
    }

    IEnumerator destroybicho(Collider other)
    {
        yield return new WaitForSeconds(4);
        Destroy(other.gameObject);

    }


}
