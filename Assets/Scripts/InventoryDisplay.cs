using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public CharacterAttributes attributes;
    public GameObject itemPrefab;

    void OnEnable()
    {
        EventManager.OnItemPickUp += OnPickUp;
    }

    void OnDisable()
    {
        EventManager.OnItemPickUp -= OnPickUp;
    }

    void OnPickUp(Item item)
    {
        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
            if (child.gameObject != gameObject)
                Destroy(child.gameObject);
        
        
        var inventory = attributes.collectedItems;
        foreach (var i in inventory)
        { 
            var itemDisplay = CreateItemDisplay(i.Key, i.Value);
        }
    }

    GameObject CreateItemDisplay(Item item, int count)
    {
        var itemDisplay = Instantiate(itemPrefab, transform);
        itemDisplay.GetComponent<Image>().sprite = item.Icon;
        itemDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count.ToString();
        return itemDisplay;
    }
}
