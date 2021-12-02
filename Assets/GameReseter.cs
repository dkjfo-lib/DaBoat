using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReseter : MonoBehaviour
{
    public DefaultClampedValue[] defaultClampedValues;
    public DefaultBoolValue[] DefaultBoolValues;

    void Awake()
    {
        foreach (var clv in defaultClampedValues)
        {
            clv.clampedValue.maxValue = clv.defaultMaxValue;
            clv.clampedValue.value = clv.defaultValue;
        }
        foreach (var bov in DefaultBoolValues)
        {
            bov.boolValue.value = bov.defaultValue;
        }
    }
}

[System.Serializable]
public class DefaultClampedValue
{
    public ClampedValue clampedValue;
    public float defaultMaxValue = 10;
    public float defaultValue = 0;
}

[System.Serializable]
public class DefaultBoolValue
{
    public Bool boolValue;
    public bool defaultValue = false;
}