using UnityEngine;

public class Hospital : MonoBehaviour
{
    public GameObject hpPotionGameObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hpPotionGameObject.SetActive(!hpPotionGameObject.activeSelf);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hpPotionGameObject.SetActive(!hpPotionGameObject.activeSelf);
    }
}
