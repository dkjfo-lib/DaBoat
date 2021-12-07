using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    new public Light light;
    public float maxRange = 1f;
    [Space]
    [Range(0, 1)] public float startingOffset = .5f;
    public float timeSpeed = .5f;
    public float maxScale = 1f;

    void Update()
    {
        var timeValue = Time.timeSinceLevelLoad * timeSpeed * Mathf.PI + startingOffset * Mathf.PI;
        var value = Mathf.Abs(Mathf.Sin(timeValue));
        transform.localScale = Vector3.one * value * maxScale;
        light.range = value * maxRange;
    }
}
