using UnityEngine;
using UnityEngine.UI;

public class StatObject
{
    private Text text;
    private Button button;

    public StatObject(Text text, Button button, string textValue)
    {
        this.text = text;
        this.button = button;
        this.text.text = textValue;
    }

    public void ToggleButton()
    {
        button.interactable = !button.interactable;
    }

    public float UpgradeStat(float addedValue)
    {
        float oldValue = float.Parse(text.text);

        text.text = (oldValue + addedValue).ToString();

        return float.Parse(text.text);
    }

    public float GetStat()
    {
        return float.Parse(text.text);
    }
}
