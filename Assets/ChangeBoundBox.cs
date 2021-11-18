using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBoundBox : MonoBehaviour
{
    public float size = 170;

    void Start()
    {
        var mesh = GetComponent<MeshFilter>();
        mesh.mesh.bounds = new Bounds(transform.position, Vector3.one * size);
    }
}
