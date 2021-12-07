using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotatingValue : MonoBehaviour
{
    public ClampedValue MonitoredNumber;
    public GameObject axis;
    public float angleMaxValue = 0;
    public float angleMinValue = -180;

    float localvalue = -12903;
    float localmaxvalue = -12903;

    void Start()
    {
        StartCoroutine(MonitorNumber());
    }

    IEnumerator MonitorNumber()
    {
        while (true)
        {
            yield return new WaitUntil(() => MonitoredNumber.value != localvalue || MonitoredNumber.maxValue != localmaxvalue);

            localvalue = MonitoredNumber.value;
            localmaxvalue = MonitoredNumber.maxValue;

            var valuePercent = localvalue / localmaxvalue;
            var angle = Mathf.Lerp(angleMinValue, angleMaxValue, valuePercent);

            axis.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
