using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyHitItem", menuName = "Scriptable Objects/EnemyHitItem")]
public class EnemyHitItem : Item
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
            IsSubbed = true;
            EventManager.OnEnemyHit += HitEnemy;
        }
    }

    public override void OnRemoved(CharacterAttributes attributes)
    {
        StackCount--;
        if (StackCount <= 0)
        {
            EventManager.OnEnemyHit -= HitEnemy;
            IsSubbed = false;
        }
    }

    public virtual void HitEnemy(GameObject source, GameObject target, float amount)
    {
        Debug.Log(source + "damaged enemy!");
    }
}
