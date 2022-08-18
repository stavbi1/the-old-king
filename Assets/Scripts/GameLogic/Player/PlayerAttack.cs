using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = transform.parent.gameObject.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("attackable"))
        {
            this.player.AddAttackable(collision.GetInstanceID() ,collision.GetComponent<IAttackable>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("attackable"))
        {
            this.player.RemoveAttackable(collision.GetInstanceID());
        }
    }
}
