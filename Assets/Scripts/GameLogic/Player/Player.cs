using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public GameObject gameOverUI;
    public GameObject goldText;
    public PlayerStats playerStats;
    public AudioClip coinSound;
    public AudioClip swordSound;

    private const float PLAYER_MOVEMENT_DRAG = 0.1f;
    private const float ATTACK_TRIGGER_DELAY = 0.5f;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private Dictionary<int, IAttackable> attackables;
    private GoldBar goldBar;
    private bool isColliding;

    public new void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        goldBar = goldText.GetComponent<GoldBar>();
        attackables = new Dictionary<int, IAttackable>();
        playerStats.Init(maxHP, speed);

        base.Start();
    }

    public override void DamageSelf(float damage)
    {
        base.DamageSelf(damage);

        playerStats.SetCurrentHP(this.currentHP);
    }

    public override void AddHP(float addedHP)
    {
        base.AddHP(addedHP);

        playerStats.SetCurrentHP(this.currentHP);
    }

    public void AddAttackable(int instanceId, IAttackable attackable)
    {
        if (!attackables.ContainsKey(instanceId))
        {
            attackables.Add(instanceId, attackable);
        }
    }

    public void RemoveAttackable(int instanceId)
    {
        attackables.Remove(instanceId);
    }

    public void OnMobKilled(float expGained)
    {
        playerStats.AddExp(expGained);
    }

    public void UpgradeStat(int index)
    {
        StatIndex statIndex = (StatIndex)index;
        float newValue = playerStats.UpgradeStat(statIndex);

        switch (statIndex)
        {
            case StatIndex.HP:
                maxHP = currentHP = newValue;
                break;
            case StatIndex.SPEED:
                speed = newValue;
                break;
            default:
                break;
        }
    }

    protected void Update()
    {
        HandleMovement();
        HandleAttack();
        HandleJump();

        isColliding = false;
    }

    protected override void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger("attack");

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                AudioPlayer.PlayClip2D(swordSound);

                if (attackables.Count > 0)
                {
                    Invoke("DamageAttackables", ATTACK_TRIGGER_DELAY);
                }
            }
        }
    }

    protected override void HandleMovement()
    {
        float horizontalTranslation = Input.GetAxis("Horizontal") * PLAYER_MOVEMENT_DRAG * base.GetSpeed();
        float midAirControl = IsGrounded() ? 1f : 0.6f;
        Vector3 directionVector = rigidBody.transform.localScale;
        
        
        float newXdirection;

        if (horizontalTranslation > 0)
        {
            newXdirection =  Common.Common.Negify(directionVector.x);
            rigidBody.transform.localScale = new Vector3(newXdirection, directionVector.y, directionVector.z);
        }
        else if (horizontalTranslation < 0)
        {
            newXdirection = Common.Common.Posify(directionVector.x);
            rigidBody.transform.localScale = new Vector3(newXdirection, directionVector.y, directionVector.z);
        }

        rigidBody.transform.Translate(horizontalTranslation * midAirControl, 0, 0);
        animator.SetFloat("speed", Math.Abs(Input.GetAxis("Horizontal")));
    }

    public override void Die()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown("space") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(0, 0);

            rigidBody.AddForce(Vector2.up * playerStats.GetJumpForce(), ForceMode2D.Impulse);
            animator.SetBool("jump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            animator.SetBool("jump", false);
        }
    }

    private bool IsGrounded()
    {
        return rigidBody.velocity.y == 0;
    }

    private void DamageAttackables()
    {
        if (attackables.Count > 0)
        {
            List<IAttackable> tempAttackables = new List<IAttackable>();
            tempAttackables.AddRange(attackables.Values);
            foreach (IAttackable attackable in tempAttackables)
            {
                attackable.DamageSelf(playerStats.GetDamage());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;

        if (collision.gameObject.CompareTag("coin"))
        {
            goldBar.AddGold(1);
            AudioPlayer.PlayClip2D(coinSound);
            Destroy(collision.gameObject);
        }
    }

    
}
