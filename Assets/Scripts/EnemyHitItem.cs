using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyHitItem", menuName = "Scriptable Objects/EnemyHitItem")]
public class EnemyHitItem : Item
{
    public override void OnPickUp(CharacterAttributes attributes)
    {
        EventManager.OnEnemyHit += HitEnemy;
    }

    public override void OnRemoved(CharacterAttributes attributes)
    {
        EventManager.OnEnemyHit -= HitEnemy;
    }

    public virtual void HitEnemy(GameObject source, GameObject target, float amount)
    {
        Debug.Log(source + "damaged enemy!");
    }
}
