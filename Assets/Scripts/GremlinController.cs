using UnityEngine;

public class GremlinController : EnemyController
{
   public Animator animator;
   public override void EnemyIdle()
   {
      // Choose a random position that is not in a wall
      Vector2 destination;
      int attempts = 0;
      int maxAttempts = 10;

      do
      {
         destination = (Vector2)transform.position + (transform.right * new Vector2(Random.Range(-3, 3), 0));
         attempts++;
      } while (Physics.CheckSphere(destination, 0.25f) && attempts < maxAttempts);

      if (attempts >= maxAttempts)
      {
         destination = transform.position;
      }

      attempts = 0;
      maxAttempts = 50;
      // Move to that position
      while (Vector2.Distance(transform.position, destination) > 0.25f && attempts < maxAttempts)
      {
         _rb.AddForce(((Vector2)transform.position - destination).normalized);
         attempts++;
      }
      
   }

   public override void EnemyChase()
   {
      
   }

   public override void EnemyAttack()
   {
      
   }
}
