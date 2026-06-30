using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject, GameObject, int> OnArmorHit;
    public static event Action<int> OnArmorUpdate;

    public static event Action<Vector2> OnCheckPointUpdate;

    public static void ArmorHit(GameObject target, GameObject attacker, int damage)
    {
        OnArmorHit?.Invoke(target, attacker, damage);
    }

    public static void ArmorUpdate(int totalSlots)
    {
        OnArmorUpdate?.Invoke(totalSlots);
    }

    public static void CheckPointUpdate(Vector2 position)
    {
        OnCheckPointUpdate?.Invoke(position);
    }
    
}
