using Enums;
using Unity.VisualScripting;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float damage;

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().Damage(damage, DamageType.Piercing, -(transform.position - other.gameObject.transform.position).normalized * 20);
        }
    }
}
