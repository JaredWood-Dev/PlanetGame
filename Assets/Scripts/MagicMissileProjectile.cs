using Enums;
using UnityEngine;

public class MagicMissileProjectile : MonoBehaviour
{
    public float missileDamage = 5f;
    public float missileSpeed = 5f;
    public GameObject shooter;

    void Start()
    {
        
    }

    void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
            Destroy(gameObject);

        if (enemies.Length > 0)
        {
            var targetPos = enemies[0].transform.position;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, missileSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().Damage(5, DamageType.Force, transform.right * 5, shooter);
            Destroy(gameObject);
        }
    }
}
