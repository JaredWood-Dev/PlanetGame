using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    [Header("Inventory")]
    public List<Item> collectedItems = new List<Item>();

    [Header("Attributes")] 
    public Dictionary<Enums.Attributes, float> Attributes = new Dictionary<Enums.Attributes, float>();

    private PlayerController _plrController;

    void Awake()
    {
        Attributes[Enums.Attributes.MaxHealth] = 100f;
        Attributes[Enums.Attributes.MovementSpeed] = 100f;
    }
    
    void Start()
    {
        //Get Initial Values from Controller
        _plrController = GetComponent<PlayerController>();
        if (_plrController != null)
        {
            Attributes[Enums.Attributes.MovementSpeed] = _plrController.speed;
            Attributes[Enums.Attributes.JumpHeight] = _plrController.jumpHeight;
            Attributes[Enums.Attributes.AbilityCoolDown] = _plrController.abilityCoolDown;
        }
    }

    private void OnEnable()
    {
        EventManager.OnItemPickUp += CollectItem;
    }

    private void OnDisable()
    {
        EventManager.OnItemPickUp -= CollectItem;
    }
    
    
    public void CollectItem(Item item)
    {
        item.OnPickUp(this);
        UpdateStats();
        collectedItems.Add(item);
    }

    public void RemoveItem(Item item)
    {
        item.OnRemoved(this);
        UpdateStats();
        collectedItems.Remove(item);
    }

    //Handles updating all the attributes this script handles.
    public void UpdateStats()
    {
        _plrController.speed = Attributes[Enums.Attributes.MovementSpeed];
    }
}
