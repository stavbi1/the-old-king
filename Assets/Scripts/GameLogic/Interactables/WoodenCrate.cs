using UnityEngine;

public class WoodenCrate : MonoBehaviour, IAttackable
{
    public GameObject coin;
    public AudioClip crateDamaged;

    private int hittingTimes;

    private void Start()
    {
        hittingTimes = 3;
    }

    public void DamageSelf(float damage)
    {
        AudioSource.PlayClipAtPoint(crateDamaged, transform.position);
        hittingTimes--;

        if (hittingTimes <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        Instantiate(coin, transform.position, Quaternion.identity);
    }
}
