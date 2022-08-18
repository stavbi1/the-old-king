using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : SliderBar
{
    protected override string TextValue(float maxValue, float currentValue)
    {
        return ((currentValue / maxValue) * 100).ToString("0.##") + "%";
    }
}
