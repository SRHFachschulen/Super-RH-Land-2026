using System;
using UnityEngine;

public class Spiky : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        var oc = other.GetComponent<OoliController>();
        if (oc == null) return;
        oc.Respawn();
    }
}
