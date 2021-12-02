using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    public ClampedValue garbageCapacity;
    public ClampedValue fuelCapacity;
    [Space]
    public Text textBuyFuel;
    public Text textSellGar;
    public Text textUpdFuel;
    public Text textUpdGar;

    private void Update()
    {
        var fuelNeeded = fuelCapacity.maxValue - fuelCapacity.value;
        textBuyFuel.text = $"-{(fuelNeeded * 0.6f).ToString("00")}$";

        var garValue = garbageCapacity.value * 10;
        textSellGar.text = $"+{(garValue).ToString("00")}$";

        var upFC = fuelCapacity.maxValue * fuelCapacity.maxValue / 30f;
        textUpdFuel.text = $"-{(upFC).ToString("00")}$";

        var upFG = (garbageCapacity.maxValue * garbageCapacity.maxValue + 28) / 2;
        textUpdGar.text = $"-{(upFG).ToString("00")}$";
    }
}
