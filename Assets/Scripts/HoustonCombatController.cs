using Enums;
using UnityEngine;

public class HoustonCombatController : CombatController
{
    /*
     * The Combat Controller for Houston.
     * Primary Attack: Thunder Gauntlets
     * Secondary Attack: Uppercut
     */

    public float attackRange;

    public override void PrimaryAttack()
    {
        if (CoolDownTimer >= attackCoolDown)
        {
            var targets = Physics2D.OverlapCircleAll(transform.position + (transform.right * (attackRange * Dir)),
                attackRange, targetLayers);

            foreach (var target in targets)
            {
                Health targetHealth = target.GetComponent<Health>();
                if (targetHealth)
                {
                    targetHealth.Damage(damage, DamageType.Thunder, transform.right * (Dir * knockback), gameObject);
                }
            }

            Animator.SetTrigger("attacked");
            CoolDownTimer = 0;
        }
    }

    public override void SecondaryAttack()
    {
        var attackRay = Physics2D.Raycast(transform.position, transform.right, attackRange, targetLayers);
        if (attackRay.collider)
        {
            var healthComponent = attackRay.collider.gameObject.GetComponent<Health>();
            if (healthComponent)
            {
                healthComponent.Damage(damage * 1.5f, DamageType.Thunder, transform.up * (knockback * 2.5f), gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position + (transform.right * 2), attackRange);
        Gizmos.DrawWireSphere(transform.position + (transform.right * attackRange * Dir), attackRange);
    }
}
