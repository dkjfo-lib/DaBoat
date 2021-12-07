using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public Transform sun;
    public GameObject lights;
    public float dotDayValue = 0;

    private void Update()
    {
        var dotDay = Vector3.Dot(sun.forward, -transform.position.normalized);
        lights.SetActive(dotDay < dotDayValue);
    }
}
