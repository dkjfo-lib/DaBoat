using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : ObjOnSphere
{
    public float timeToLive = 10f;
    public float shrinkSpeed = .1f;
    float timeLives = 10f;

    void Start()
    {
        ConfigurePosition();
    }

    void Update()
    {
        UpdatePosition();
        Wiggle();
        SelfDestruct();
    }

    void Wiggle()
    {
        var timeValue = Time.timeSinceLevelLoad * Mathf.PI * wiggleSpeed;
        var wiggleValue = Mathf.Sin(timeValue);
        var wiggleAngle = wiggleValue * wiggleDegrees;
        transform.localRotation = Quaternion.Euler(new Vector3(wiggleAngle, 0, 0));
    }

    void SelfDestruct()
    {
        timeLives += Time.deltaTime;
        if (timeLives > timeToLive)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            if (transform.localScale.x < 0 || transform.localScale.y < 0 || transform.localScale.z < 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
