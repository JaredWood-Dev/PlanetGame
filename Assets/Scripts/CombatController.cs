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

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PrimaryAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            SecondaryAttack();
        }
    }

    public virtual void PrimaryAttack()
    {
        print("Used Primary Attack!");
    }

    public virtual void SecondaryAttack()
    {
        print("Used Secondary Attack!");
    }
}
