using UnityEngine;
using UnityEngine.UI;

public class HPPotion : MonoBehaviour
{
    public GameObject goldBarGameObject;
    public GameObject tooltip;
    public GameObject playerGameObject;
    public Text tooltipText;
    public Text potionPrice;
    public AudioClip swallowSound;

    private GoldBar goldBar;
    private Player player;
    private const int PRICE = 3;
    private const float REFILL_AMOUNT = 20;

    private void Start()
    {
        player = playerGameObject.GetComponent<Player>();
        goldBar = goldBarGameObject.GetComponent<GoldBar>();
        potionPrice.text = PRICE + "$";
    }

    private string GetMessage()
    {
        if (goldBar.GetGoldAmount() < PRICE)
        {
            return "Not enough gold";
        }
        else
        {
            return "Refills " + REFILL_AMOUNT + " HP";
        }
    }

    public void ToggleTooltip()
    {
        if (!tooltip.activeSelf) // if next state will be active
        {
            tooltipText.text = GetMessage();
        }

        tooltip.SetActive(!tooltip.activeSelf);
    }

    public void Buy()
    {
        if (goldBar.GetGoldAmount() >= PRICE)
        {
            AudioPlayer.PlayClip2D(swallowSound);
            goldBar.AddGold(-PRICE);
            player.AddHP(REFILL_AMOUNT);
            tooltipText.text = GetMessage();
        }
    }
}
