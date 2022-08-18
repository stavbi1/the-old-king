using UnityEngine;

public class Wolf : Mob
{
    private static float DAMAGE = 2f;

    protected override float killingExp => 25f;

    protected override void HandleAttack()
    {
        this.animator.SetTrigger("attack");

        if (Time.time > base.attackTime)
        {
            base.attackTime += base.attackPeriod;
            base.target.DamageSelf(DAMAGE);
            AudioPlayer.PlayClip2D(mobAttackSound);
        }

    }

    protected void Update()
    {
        this.animator.ResetTrigger("attack");
        if (base.InsideArea())
        {
            switch (base.currentState)
            {
                case Mob.State.CHASE:
                    base.HandleChase();
                    break;
                case Mob.State.ATTACK:
                    this.HandleAttack();
                    break;
                default:
                    base.HandleMovement();
                    break;
            }
        } else // return to wolf area even if provoked
        {
            base.HandleMovement();
        }
    }
}
