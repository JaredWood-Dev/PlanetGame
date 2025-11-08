using System;
using Enums;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    /*
     * The Primary Controller for the EnemyAI
     * This implementation is using a Finite State Machine (FSM)
     */
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    public EnemyState currentState = EnemyState.Idle;
    public float aggroRange;
    public float meleeRange;
    public Transform player;
    public float attackCooldown;
    private float _attackCoolDownTimer;

    private void Update()
    {
        if (player)
        {
            switch (currentState)
            {
                case EnemyState.Idle:
                    if (Vector2.Distance(player.position, transform.position) <= aggroRange * 2)
                    {
                        currentState = EnemyState.Patrol;
                    }

                    EnemyIdle();
                    break;
                case
                    EnemyState.Patrol:
                    if (Vector2.Distance(player.position, transform.position) <= aggroRange)
                    {
                        currentState = EnemyState.Chase;
                    }

                    EnemyPatrol();
                    break;
                case EnemyState.Chase:
                    if (Vector2.Distance(player.position, transform.position) <= meleeRange)
                    {
                        currentState = EnemyState.Attack;
                    }

                    EnemyChase();
                    break;
                case EnemyState.Attack:
                    EnemyAttack();
                    break;
            }

            if (Vector2.Distance(player.position, transform.position) > aggroRange)
            {
                currentState = EnemyState.Idle;
            }
        }
    }

    void FixedUpdate()
    {
        _attackCoolDownTimer += Time.fixedDeltaTime;
    }

    void EnemyIdle()
    {
        print("Idle");
    }

    void EnemyPatrol()
    {
        print("Patrol");
    }

    void EnemyChase()
    {
        print("Chase");
        transform.position = Vector2.MoveTowards(transform.position, player.position, 5 * Time.fixedDeltaTime);
    }

    void EnemyAttack()
    {
        if (_attackCoolDownTimer >= attackCooldown)
        {
            player.GetComponent<Health>().Damage(10, DamageType.Bludgeoning,
                -(transform.position - player.position) * 10, gameObject);
            currentState = EnemyState.Chase;
            _attackCoolDownTimer = 0f;
        }
    }
}
