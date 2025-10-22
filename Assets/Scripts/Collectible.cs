using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Item itemData;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.Icon;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.ItemCollected(itemData);
            Destroy(gameObject);
        }
    }
}
