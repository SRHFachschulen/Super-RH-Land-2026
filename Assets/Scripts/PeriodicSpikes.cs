using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PeriodicSpikes : MonoBehaviour{
    private float upY = 0;
    private float downY = -0.2f;

    public Transform theSpikes;

    public float phaseShift;
    public float frequency;

    // Update is called once per frame
    void Update(){
        float sinVal = Mathf.Sin(phaseShift + (Time.time * 2 * Mathf.PI * frequency));
        var childLocalPosition = theSpikes.localPosition;
        childLocalPosition.y = sinVal > 0 ? upY : downY;
        theSpikes.localPosition = childLocalPosition;
    }
}