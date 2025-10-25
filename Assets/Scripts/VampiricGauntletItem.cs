using UnityEngine;

[CreateAssetMenu(fileName = "VampiricGauntletItem", menuName = "Scriptable Objects/VampiricGauntletItem")]
public class VampiricGauntletItem : EnemyHitItem
{
    public float lifeStealPercentage;
    public override void HitEnemy(GameObject source, GameObject target, float damage)
    {
        source.GetComponent<Health>().Heal(damage * lifeStealPercentage);
    }
}
