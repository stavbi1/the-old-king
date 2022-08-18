using UnityEngine;
using UnityEngine.UI;

public abstract class SliderBar : MonoBehaviour
{
    public Slider slider;
    public Text text;

    protected abstract string TextValue(float maxValue, float currentValue);

    public void Init(float maxValue, float initValue)
    {
        slider.maxValue = maxValue;
        slider.value = initValue;
        text.text = TextValue(maxValue, initValue);
    }

    public void SetValue(float value)
    {
        slider.value = value;
        text.text = TextValue(slider.maxValue, value);
    }

    public void Fulfill()
    {
        slider.value = slider.maxValue;
        text.text = TextValue(slider.maxValue, slider.value);
    }
}