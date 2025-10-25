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
            var attackRay = Physics2D.Raycast(transform.position, transform.right * Dir, attackRange, targetLayers);
            if (attackRay.collider)
            {
                var healthComponent = attackRay.collider.gameObject.GetComponent<Health>();
                if (healthComponent)
                {
                    healthComponent.Damage(damage, DamageType.Thunder, transform.right * knockback, gameObject);
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
}
