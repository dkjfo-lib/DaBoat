using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : ObjOnSphere
{
    public Bool isPlayerInThePort;
    [Space]
    public bool isPlayerNear = false;

    void Start()
    {
        ConfigurePosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInThePort.value = true;
        isPlayerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerInThePort.value = false;
        isPlayerNear = false;
    }
}
