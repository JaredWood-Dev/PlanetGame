using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    /*
     * This script handles creating and delegating events.
     */
    
    public static event Action<Item> OnItemPickUp;
    public static Action<Enums.Attributes, float> UpdateStats;

    public static void ItemCollected(Item item)
    {
        OnItemPickUp?.Invoke(item);
    }

    public static void StatUpdated(Enums.Attributes attribute, float value)
    {
        UpdateStats?.Invoke(attribute, value);
    }
}
