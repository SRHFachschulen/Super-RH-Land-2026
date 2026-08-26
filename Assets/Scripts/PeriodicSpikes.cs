using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PeriodicSpikes : MonoBehaviour{
    private float upY = 0;
    private float downY = -0.2f;
    private float spikeSpeed = 3;

    public Transform theSpikes;

    public float phaseShift;
    public float frequency;

    // Update is called once per frame
    void Update(){
        float sinVal = Mathf.Sin(phaseShift + (Time.time * 2 * Mathf.PI * frequency));
        var childLocalPosition = theSpikes.localPosition;
        childLocalPosition.y += spikeSpeed * Time.deltaTime * (sinVal > 0 ? 1 : -1);
        childLocalPosition.y = Mathf.Clamp(childLocalPosition.y, downY, upY);
        theSpikes.GetComponent<Collider>().enabled = !(childLocalPosition.y <= (downY+upY/2));
        theSpikes.localPosition = childLocalPosition;
    }
}