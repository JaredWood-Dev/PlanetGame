using System;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MetorEnemyController : MonoBehaviour
{
    public enum Status
    {
        Wander,
        Chase,
        Dash,
        Stun
    }
    public Status meteorStatus;
    public float meteorSpeed;
    public Vector2 wanderTarget;
    public float timeOut;
    private float _timeOutTimer;
    public float attackDistance;
    public float followDistance;
    public bool canCollide = false;
    public float dashTime;
    private float _dashTimer;
    private Vector2 _tempPos;
    public GameObject trigger;

    public GameObject player;
    private Rigidbody2D _rb;

    void Awake()
    {
        trigger = transform.GetChild(0).gameObject;
    }
    
    void Start()
    {
        wanderTarget = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
       
        switch (meteorStatus)
        {
            case Status.Wander:
                Wander();
                if (Vector2.Distance(transform.position, player.transform.position) < followDistance)
                {
                    meteorStatus = Status.Chase;
                    _dashTimer = 0f;
                }
                break;
            case Status.Chase:
                Follow(player.transform);
                if (Vector2.Distance(transform.position, player.transform.position) < attackDistance + 1 &&
                    _dashTimer >= dashTime)
                {
                     meteorStatus = Status.Dash;
                     _tempPos = player.transform.position;
                }
                   
                break;
            case Status.Dash:
                Dash(_tempPos);
                Invoke("Stunned", 1f);
                break;
            case Status.Stun:
                Stunned();
                break;
        }
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attackDistance + 1)
        {
            _timeOutTimer += Time.deltaTime;
        }
        else
        {
            _timeOutTimer = 0f;
        }
    }
    
    //Meteor chooses a random spot to travel to in the world
    void Wander()
    {
        //If the Meteor reaches the wander target, choose a new one
        if (Vector2.Distance(wanderTarget, transform.position) <= 1.5f || _timeOutTimer >= timeOut)
        {
            float randomRange = 5;
            _timeOutTimer = 0;
            Vector2 targetPoint = (Vector2)transform.position + new Vector2(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange));
            while (Physics2D.OverlapPoint(targetPoint))
            {
                targetPoint = (Vector2)transform.position + new Vector2(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange));
                //print("attempted to move into object object!");
            }
            wanderTarget = targetPoint;
            Vector2 direction = ((Vector2)transform.position - wanderTarget).normalized;
            float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot);
        }
        transform.position = Vector2.Lerp(transform.position, wanderTarget, Time.deltaTime * (meteorSpeed / 10));
    }

    //The meteorite follows the player, and orbits around them.
    void Follow(Transform target)
    {
        Vector2 direction = -(target.position - transform.position).normalized;
        Vector2 targetPos = (Vector2)target.transform.position + (direction * attackDistance);
        _dashTimer += Time.deltaTime;
        //print(targetPos);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * meteorSpeed);
    }

    // The meteorite dashes into the player, while dashing, it can hurt the player
    void Dash(Vector2 targetPos)
    {
        Vector2 direction = (targetPos - (Vector2)gameObject.transform.position).normalized;
        Vector2 speed = direction * (meteorSpeed * 50f);
        trigger.GetComponent<DamageTrigger>().canCollide = true;
        //_rb.linearVelocity = speed;
        _rb.AddForce(speed, ForceMode2D.Impulse);
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot);
    }

    //While stunned, the meteorite can't do anything
    void Stunned()
    {
        meteorStatus = Status.Stun;
        trigger.GetComponent<DamageTrigger>().canCollide = false;
        Invoke("Recover", 5f);
    }

    void Recover()
    {
        meteorStatus = Status.Wander;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canCollide)
        {
            trigger.GetComponent<DamageTrigger>().canCollide = false;
            meteorStatus = Status.Stun;
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
