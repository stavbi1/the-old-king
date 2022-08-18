using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
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
            this.mob.setState(Mob.State.ATTACK);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("player"))
        {
            this.mob.setState(Mob.State.CHASE);
        }
    }
}
