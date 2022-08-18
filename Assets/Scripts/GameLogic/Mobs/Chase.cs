using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    private Mob mob;

    // Start is called before the first frame update
    void Start()
    {
        this.mob = transform.parent.gameObject.GetComponent<Mob>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("player"))
        {
            this.mob.setTarget(collision.gameObject.GetComponent<Player>());
            this.mob.setState(Mob.State.CHASE);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("player"))
        {
            this.mob.setTarget(null);
            this.mob.setState(Mob.State.WALK);
        }
    }
}
