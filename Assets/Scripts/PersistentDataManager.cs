using System.Collections.Generic;
using UnityEngine;

public class PersistentDataManager : MonoBehaviour
{
    public static PersistentDataManager Instance;
    public static Dictionary<Item, int> Items;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SaveItems(Dictionary<Item, int> i)
    {
        Items = new Dictionary<Item, int>(i);
    }

    public void ClearItems()
    {
        Items.Clear();
    }
}
