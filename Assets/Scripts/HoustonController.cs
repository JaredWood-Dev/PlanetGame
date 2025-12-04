using Enums;
using UnityEngine;

public class HoustonController : PlayerController
{
   [Header("Breath Weapon")] 
   public float breathDistance;
   public float breathHeight;
   public float breathForce;
   public float breathDamageMultiplier; //This is so its still based of damage, which can be increased with items
   private HoustonCombatController _hcc;
   protected float Rot = 0;
   
   public override void SpecialAbility()
   {
      //Cache the Combat Controller, as it's needed for this
      if (!_hcc)
         _hcc = GetComponent<HoustonCombatController>();
      
      //Send Out a Ray from Houston, if it hits the ground, then launch Houston in the opposite direction,
      //with a force relative to how far away the ground was
      Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 diff = mousePos - (Vector2)transform.position;
      var breathRay = Physics2D.Raycast(transform.position, diff.normalized, breathDistance, groundLayer);
      Rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
      AbilitySystem.StartSystem();
      AbilitySystem.targetSystem.transform.rotation = Quaternion.Euler(-Rot, 90, 0f);
      AbilitySystem.transform.position = gameObject.transform.position + (transform.up * -0.5f);
      if (breathRay.collider)
      {
         gameObject.GetComponent<Rigidbody2D>().AddForce(-diff.normalized * (breathForce), ForceMode2D.Impulse);
      }
      
      
      Invoke("DamageEnemies", 0.1f);
   }

   void DamageEnemies()
   {
      var breathAOE = Physics2D.OverlapBoxAll((Vector2)transform.position + (breathDistance / 2f * MathFunctions.DegreesToVector(Rot)), new Vector2(breathDistance, breathHeight), Rot, _hcc.targetLayers);
      foreach (var col in breathAOE)
      {
         var health = col.gameObject.GetComponent<Health>();
         if (health)
            health.Damage(_hcc.damage * breathDamageMultiplier, DamageType.Force, -(gameObject.transform.position - col.gameObject.transform.position).normalized * (breathForce * 0.01f), gameObject);
      }
   }

   void OnDrawGizmosSelected()
   {
      Gizmos.DrawWireCube(transform.position + (breathDistance / 2f * transform.right), new Vector2(breathDistance, breathHeight));
   }
}
