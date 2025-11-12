using Enums;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public float damage;
    public bool canCollide = false;
    public GameObject parent;

    void Awake()
    {
        parent = transform.parent.gameObject;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (canCollide)
        {
            if (other.CompareTag("Player"))
            {
                canCollide = false;
                other.gameObject.GetComponent<Health>().Damage(damage, DamageType.Bludgeoning, other.gameObject.transform.up * 10, parent);
                parent.GetComponent<MetorEnemyController>().meteorStatus = MetorEnemyController.Status.Stun;
                parent.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
        }
    }
}
