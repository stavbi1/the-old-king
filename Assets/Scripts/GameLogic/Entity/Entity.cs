using UnityEngine;

public abstract class Entity : MonoBehaviour, IAttackable
{
    public float speed = 0.05f;
    public float maxHP = 100f;

    protected float currentHP;

    public void Start()
    {
        currentHP = maxHP;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    public float GetCurrentHP()
    {
        return currentHP;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void FulfillHP()
    {
        currentHP = maxHP;
    }

    public virtual void DamageSelf(float damage)
    {
        if (gameObject != null)
        {
            if (currentHP - damage <= 0)
            {
                currentHP = 0;
                Die();
            }
            else
            {
                currentHP -= damage;
            }
        }
    }

    public virtual void AddHP(float addedHP)
    {
        if (currentHP + addedHP > maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += addedHP;
        }
    }

    protected abstract void HandleAttack();
    protected abstract void HandleMovement();
    public abstract void Die();
    
}
