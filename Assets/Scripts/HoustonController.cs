using UnityEngine;

public class HoustonController : PlayerController
{
   [Header("Breath Weapon")] 
   public float breathDistance;
   public float breathForce;
   public float breathDamage;
   
   public override void SpecialAbility()
   {
      //print("Singularity Breath Weapon Used!");
      //Send Out a Ray from Houston, if it hits the ground, then launch Houston in the opposite direction,
      //with a force relative to how far away the ground was
      Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 diff = mousePos - (Vector2)transform.position;
      var breathRay = Physics2D.Raycast(transform.position, diff.normalized, breathDistance, groundLayer);
      AbilitySystem.StartSystem();
      AbilitySystem.targetSystem.transform.rotation = Quaternion.Euler(0, 0, MathFunctions.VectorToDegrees(diff));
      if (breathRay.collider)
      {
         gameObject.GetComponent<Rigidbody2D>().AddForce(-diff.normalized * (breathForce), ForceMode2D.Impulse);
      }
   }
}
