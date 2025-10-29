using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "BloodSampleItem", menuName = "Scriptable Objects/BloodSampleItem")]
public class BloodSampleItem : EnemyDeathItem
{
    public float ExplosionRadius;
    public LayerMask EffectedLayers;
    public ParticleSystem ExplosionParticles;
    public override void KilledEnemy(GameObject killer, GameObject target)
    {
        var particles = Instantiate(ExplosionParticles);
        particles.transform.position = target.transform.position;
        Destroy(particles, 0.5f);
        
        var explosionCast = Physics2D.OverlapCircleAll(target.transform.position, ExplosionRadius, EffectedLayers);
        foreach (var hitTarget in explosionCast)
        {
            if (hitTarget.gameObject != target)

                if (hitTarget.gameObject)
                {
                    Vector2 knockbackVector =
                        (hitTarget.transform.position - target.transform.position).normalized * 50;
                    hitTarget.gameObject.GetComponent<Health>().Damage(5, DamageType.Force, knockbackVector, killer);
                }
        }
        
    }
}
