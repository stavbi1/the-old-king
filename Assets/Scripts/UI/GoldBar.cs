using UnityEngine;
using UnityEngine.UI;

public class GoldBar : MonoBehaviour
{
    private int goldAmount;

    public Text goldText;

    private void Start()
    {
        goldAmount = 0;
    }

    public void AddGold(int amount)
    {
        goldAmount += amount;
        goldText.text = goldAmount.ToString();
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }
}
