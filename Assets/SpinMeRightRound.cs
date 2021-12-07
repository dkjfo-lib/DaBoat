using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMeRightRound : MonoBehaviour
{
    public Vector3 axis = Vector3.up;
    public float degreesPerSecond = 90;

    void Update()
    {
        transform.Rotate(axis, degreesPerSecond * Time.deltaTime, Space.Self);
    }
}
