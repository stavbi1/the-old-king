using UnityEngine;

public abstract class Mob : Entity
{
    public State currentState;
    public enum State { WALK, CHASE, ATTACK };
    public GameObject coin;
    public AudioClip mobDieSound;
    public AudioClip mobAttackSound;

    protected Player player;
    protected Player target;
    protected Animator animator;
    protected float attackTime = 0f;
    protected float attackPeriod = 2f;
    protected Collider2D areaCollider;

    private MobSpawn mobSpawn;
    private bool flipped = false;

    protected abstract float killingExp
    {
        get;
    }

    public new void Start()
    {
        currentState = State.WALK;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        areaCollider = transform.parent.gameObject.GetComponent<Collider2D>();
        mobSpawn = transform.parent.GetComponent<MobSpawn>();

        base.Start();
    }

    public void setState(State newState)
    {
        this.currentState = newState;

        if (newState == State.ATTACK)
        {
            this.attackTime = Time.time;
        }
    }

    public void setTarget(Player player)
    {
        this.target = player;
    }

    public override void Die()
    {
        if (gameObject != null)
        {
            SpawnDrops();
            mobSpawn.OnMobKilled();
            player.OnMobKilled(killingExp);
            AudioPlayer.PlayClip2D(mobDieSound);
            Destroy(gameObject);
        }
    }

    protected override void HandleMovement()
    {
        if (!InsideArea() && !this.flipped)
        {
            this.Flip();
        } else if (InsideArea())
        {
            flipped = false;
        }

        transform.Translate(Vector3.right * Time.deltaTime);
    }

    protected void HandleChase()
    {
        if ((target.transform.position.x - transform.position.x < 0 && Mathf.Round(transform.rotation.eulerAngles.y) == 0) ||
            (target.transform.position.x - transform.position.x > 0 && Mathf.Round(transform.rotation.eulerAngles.y) == 180))
        {

            this.Flip();
        }

        transform.Translate(Vector3.right * Time.deltaTime);
        this.flipped = false;
    }

    protected bool InsideArea()
    {
        // Mob inside area, and if chasing taget - then the target also has to be inside
        return (
            areaCollider.bounds.Contains((Vector2)transform.position) &&
            (!this.target || areaCollider.bounds.Contains((Vector2)this.target.transform.position))
        );
    }

    private void Flip()
    {
        Vector3 oldDirection = transform.rotation.eulerAngles;

        transform.Rotate(oldDirection.x, 180, oldDirection.z);
        this.flipped = true;
    }

    private void SpawnDrops()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
    }
}
