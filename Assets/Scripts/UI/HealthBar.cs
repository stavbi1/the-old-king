using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : SliderBar
{
    protected override string TextValue(float maxValue, float currentValue)
    {
        return currentValue + "/" + maxValue;
    }
}
