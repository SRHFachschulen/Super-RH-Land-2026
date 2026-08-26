using System;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour{
    [SerializeField]
    private float rotationSpeed = 90;
    [SerializeField]
    private GameObject OnCollectionFX;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other){
        var oc = other.GetComponent<OoliController>();
        if (oc == null) return;
        if (OnCollectionFX != null){
            Instantiate(OnCollectionFX, transform.position, Quaternion.identity);
        }
        // Todo call event
        Destroy(gameObject);
    }
}
