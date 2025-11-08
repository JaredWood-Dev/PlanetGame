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
            float dist = float.MaxValue;
            int smallestDistance = 0;
            for (int i = 0; i < enemies.Length; i++)
            {
                if (Vector2.Distance(transform.position, enemies[i].transform.position) < dist)
                {
                    dist = Vector2.Distance(transform.position, enemies[i].transform.position);
                    smallestDistance = i;
                }
            }
            var targetPos = enemies[smallestDistance].transform.position;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, missileSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().Damage(missileDamage, DamageType.Force, transform.right, shooter);
            Destroy(gameObject);
        }
    }
}
