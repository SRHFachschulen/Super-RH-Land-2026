using System;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour{
    public Transform RespawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (RespawnPoint == null){
            RespawnPoint = transform.GetChild(0);
        }
    }

    private void OnTriggerEnter(Collider other){
        OoliController oc = other.GetComponent<OoliController>();
        if (oc != null){
            oc.SetCheckpoint(this);
        }
    }
}
