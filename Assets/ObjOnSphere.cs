using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjOnSphere : MonoBehaviour
{
    public float wiggleSpeed = .5f;
    public float wiggleDegrees = 10f;
    public float heightPosition = 51;

    protected void ConfigurePosition()
    {
        var parent = new GameObject("host " + gameObject.name);
        parent.transform.parent = transform.parent;
        parent.transform.position = new Vector3();
        parent.transform.up = (transform.position - parent.transform.position).normalized;
        transform.parent = parent.transform;
        transform.localRotation = Quaternion.identity;
        transform.position = parent.transform.up * heightPosition;
    }

    protected void UpdatePosition()
    {
        transform.position = transform.parent.transform.up * heightPosition;
    }
}
