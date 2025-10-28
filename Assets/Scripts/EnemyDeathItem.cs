using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDeathItem", menuName = "Scriptable Objects/EnemyDeathItem")]
public class EnemyDeathItem : Item
{
    protected CharacterAttributes PlayerAttributes;
    public override void OnPickUp(CharacterAttributes attributes)
    {
        PlayerAttributes = attributes;
        EventManager.OnEnemyDied += KilledEnemy;
    }

    public override void OnRemoved(CharacterAttributes attributes)
    {
        EventManager.OnEnemyDied -= KilledEnemy;
    }

    public virtual void KilledEnemy(GameObject killer, GameObject target)
    {
        Debug.Log(killer + "killed: " + target);
    }
}
