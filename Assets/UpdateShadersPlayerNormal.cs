using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateShadersPlayerNormal : MonoBehaviour
{
    public Material[] materials;

    void Update()
    {
        foreach (var mat in materials)
        {
            mat.SetVector("PlayerUp", transform.up);
        }
    }
}
