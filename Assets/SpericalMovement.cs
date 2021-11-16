using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpericalMovement : MonoBehaviour
{
    public float angY = 0;
    public float angX = 0;
    public float height = 10;

    Transform holder;

    void Start()
    {
        //holder = new GameObject("holder " + gameObject.name).transform;
        //transform.parent = holder;
        holder = transform.parent;
    }

    void Update()
    {
        holder.rotation = Quaternion.Euler(angX, angY, 0);
        transform.localPosition = new Vector3(0, height, 0);
    }
}
