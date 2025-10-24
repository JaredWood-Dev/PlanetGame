using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    /*
     * This script handles creating and delegating events.
     */
    
    public static event Action<Item> OnItemPickUp;
    public static Action<Enums.Attributes, float> UpdateStats;
    public static event Action<GameObject> OnEnemyDied;
    public static event Action OnPlayerDied;
    public static event Action<GameObject, GameObject, float> OnEnemyHit;
    public static event Action<GameObject> OnPlayerHit;

    public static void ItemCollected(Item item)
    {
        OnItemPickUp?.Invoke(item);
    }

    public static void StatUpdated(Enums.Attributes attribute, float value)
    {
        UpdateStats?.Invoke(attribute, value);
    }

    public static void PlayerDeath()
    {
        OnPlayerDied?.Invoke();
    }

    public static void EnemyDeath(GameObject killer)
    {
        OnEnemyDied?.Invoke(killer);
    }

    public static void EnemyDamaged(GameObject source = null, GameObject target = null, float damage = 0)
    {
        OnEnemyHit?.Invoke(source, target, damage);
    }

    public static void PlayerDamaged(GameObject source = null)
    {
        OnPlayerHit?.Invoke(source);
    }
}
