using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBoundBox : MonoBehaviour
{
    public float size = 150;

    void Update()
    {
        var render = GetComponent<MeshRenderer>();
        var mesh = GetComponent<MeshFilter>();
        mesh.mesh.bounds = new Bounds(transform.position, Vector3.one * 150);
    }
}
