using System;
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
    public LayerMask targetLayers;
    public float attackCoolDown;
    protected int Dir;
    
    [NonSerialized]
    public float CoolDownTimer;
    [NonSerialized]
    public Animator Animator;
    
    void Start()
    {
        Animator = GetComponent<Animator>();
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

        print(Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Horizontal") > 0)
            Dir = 1;
        else
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
}
