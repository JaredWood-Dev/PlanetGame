using System;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatController : MonoBehaviour
{
    /*
     * This script allows player characters to make attacks against other game-objects.
     * Other scripts inherit from this one to create unique attacks for characters.
     */
    
    public float damage;
    public float knockback;
    public float critChance;
    public float critDamage;
    public LayerMask targetLayers;
    public float attackCoolDown;
    [NonSerialized]
    public int Dir = 1;
    
    [NonSerialized]
    public float CoolDownTimer;
    [NonSerialized]
    public Animator Animator;
    
    void Start()
    {
        Animator = GetComponent<Animator>();
        Animator.SetFloat("attackSpeed", 1 + (1 - attackCoolDown));
    }
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            PrimaryAttack();
        }

        if (Input.GetButton("Fire2"))
        {
            SecondaryAttack();
        }
        
        if (Input.GetAxis("Horizontal") > 0)
            Dir = 1;
        if (Input.GetAxis("Horizontal") < 0)
            Dir = -1;

    }

    public virtual void PrimaryAttack()
    {
        print("Used Primary Attack!");
    }

    public virtual void SecondaryAttack()
    {
        print("Used Secondary Attack!");
    }

    void FixedUpdate()
    {
        CoolDownTimer += Time.fixedDeltaTime;
    }

    private void OnEnable()
    {
        EventManager.UpdateStats += UpdateStats;
    }

    private void OnDisable()
    {
        EventManager.UpdateStats -= UpdateStats;
    }
    
    public void UpdateStats(Attributes stat, float value)
    {
        switch (stat)
        {
            case Attributes.AttackDamage: damage = value;break;
            case Attributes.CritChance: critChance = value;break;
            case Attributes.CritDamage: critDamage = value;break;
            case Attributes.AttackCoolDown: attackCoolDown = value;break;
            case Attributes.Knockback: knockback = value;break;
        }
        Animator.SetFloat("attackSpeed", 1 + (1 - attackCoolDown));
    }
}
