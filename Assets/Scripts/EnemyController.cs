using System;
using Enums;
using Pathfinding;
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
        Chase,
        Attack
    }

    public EnemyState currentState = EnemyState.Idle;
    public float aggroRange;
    public float meleeRange;
    public Transform player;
    public float attackCooldown;
    private float _attackCoolDownTimer;
    protected Rigidbody2D _rb;
    protected AIPathfinding _aiPath;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _aiPath = GetComponent<AIPathfinding>();
    }
    
    private void Update()
    {
        if (player)
        {
            switch (currentState)
            {
                case EnemyState.Idle:
                    if (Vector2.Distance(player.position, transform.position) <= aggroRange)
                    {
                        currentState = EnemyState.Chase;
                    }
                    EnemyIdle();
                    break;
                case EnemyState.Chase:
                    if (Vector2.Distance(player.position, transform.position) <= meleeRange)
                    {
                        if (_attackCoolDownTimer > attackCooldown)
                        {
                            currentState = EnemyState.Attack;
                            _attackCoolDownTimer = 0;
                        }
                    }
                    if (Vector2.Distance(player.position, transform.position) > aggroRange)
                    {
                        currentState = EnemyState.Idle;
                    }
                    EnemyChase();
                    break;
                case EnemyState.Attack:
                    EnemyAttack();
                    currentState = EnemyState.Idle;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        _attackCoolDownTimer += Time.fixedDeltaTime;
    }

    public virtual void EnemyIdle()
    {
        print("Idle");
    }

    public virtual void EnemyChase()
    {
        print("Chase");
    }

    public virtual void EnemyAttack()
    {
        print("Attack!");
    }
}
