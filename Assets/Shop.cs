using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Bool isPlayerInThePort;
    [Space]
    [Range(0, 1)] public float reciclingPercent = 1;
    [Range(0, 30)] public float donationsMultiplier = 10;
    [Range(0, 3)] public float fuelCost = .6f;
    [Space]
    public float fuelUpgradeModifier = 10 * 3;
    public float garCapUpgradeModifier = 2;
    [Space]
    public ClampedValue garbageCapacity;
    public ClampedValue fuelCapacity;
    public ClampedValue score;
    public ClampedValue money;

    void Update()
    {
        if (!isPlayerInThePort.value) return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            score.value += garbageCapacity.value * reciclingPercent;
            money.value += garbageCapacity.value * donationsMultiplier;
            garbageCapacity.value = 0;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            var fuelCanAffort = money.value / fuelCost;
            var fuelNeeded = fuelCapacity.maxValue - fuelCapacity.value;

            if (fuelNeeded > fuelCanAffort)
                fuelNeeded = fuelCanAffort;

            fuelCapacity.value = fuelCapacity.maxValue;
            money.value -= fuelNeeded * fuelCost;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var upgradeCost = fuelCapacity.maxValue * fuelCapacity.maxValue / fuelUpgradeModifier;
            if (upgradeCost <= money.value)
            {
                fuelCapacity.maxValue += 10;
                money.value -= upgradeCost;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var upgradeCost = (garbageCapacity.maxValue * garbageCapacity.maxValue + 28) / garCapUpgradeModifier;
            if (upgradeCost <= money.value)
            {
                garbageCapacity.maxValue += 2;
                money.value -= upgradeCost;
            }
        }
    }

}
