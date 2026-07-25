using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoustonController : PlayerController
{
   [Header("Breath Weapon")] 
   public float breathDistance;
   public float breathForce;
   public float breathDamage;

   public InputActionReference pointAction;
   public Vector2 diff = Vector2.right;
   public LocatorRotation locatorRotationScript;
   
   public override void SpecialAbility()
   {
      //print("Singularity Breath Weapon Used!");
      //Send Out a Ray from Houston, if it hits the ground, then launch Houston in the opposite direction,
      //with a force relative to how far away the ground was
      if (GameManager.isController)
      {
         //Controller Implementation
         //diff = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")).normalized;
         if (pointAction.action.phase == InputActionPhase.Started)
            diff = pointAction.action.ReadValue<Vector2>();
      }
      else
      {
         //Mouse Implementation
         Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
         diff = mousePos - (Vector2)transform.position;
         print("Mouse: " + mousePos);
         print("Player: " + transform.position);
      }

      var breathRay = Physics2D.Raycast(transform.position, diff.normalized, breathDistance, groundLayer);
      AbilitySystem.StartSystem();
      AbilitySystem.targetSystem.transform.rotation = Quaternion.Euler(0, 0, MathFunctions.VectorToDegrees(diff));
      if (breathRay.collider)
      {
         gameObject.GetComponent<Rigidbody2D>().AddForce(-diff.normalized * (breathForce), ForceMode2D.Impulse);
         BreathEffect.Play();
      }
      
      print(MathFunctions.VectorToDegrees(diff));
   }
}
