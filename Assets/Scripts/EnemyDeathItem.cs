using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDeathItem", menuName = "Scriptable Objects/EnemyDeathItem")]
public class EnemyDeathItem : Item
{
    protected CharacterAttributes PlayerAttributes;
    protected static bool IsSubbed = false;
    protected static int StackCount = 0;
    public override void OnPickUp(CharacterAttributes attributes)
    {
        PlayerAttributes = attributes;
        StackCount++;
        if (!IsSubbed)
        {
            EventManager.OnEnemyDied += KilledEnemy;
        }
    }

    public override void OnRemoved(CharacterAttributes attributes)
    {
        StackCount--;
        if (StackCount <= 0)
        {
            EventManager.OnEnemyDied -= KilledEnemy;
            IsSubbed = false;
        }
    }

    public virtual void KilledEnemy(GameObject killer, GameObject target)
    {
        Debug.Log(killer + "killed: " + target);
    }
}
