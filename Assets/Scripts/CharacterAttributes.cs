using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    [Header("Inventory")]
    public Dictionary<Item, int> collectedItems = new Dictionary<Item, int>();

    [Header("Attributes")] 
    public Dictionary<Enums.Attributes, float> Attributes = new Dictionary<Enums.Attributes, float>();

    private PlayerController _plrController;
    private Health _plrHealth;

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
        _plrHealth = GetComponent<Health>();
        if (_plrHealth != null)
        {
            Attributes[Enums.Attributes.MaxHealth] = _plrHealth.maxHealth;
            Attributes[Enums.Attributes.Defense] = _plrHealth.defense;
            Attributes[Enums.Attributes.HeathRegen] = _plrHealth.regenRate;
        }

        Attributes[Enums.Attributes.AttackCoolDown] = 1f;
        Attributes[Enums.Attributes.AttackDamage] = 10;
        StartCoroutine(StartTimers());
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
        if (collectedItems.ContainsKey(item))
            collectedItems[item]++;
        else
            collectedItems.Add(item, 1);
        UpdateStats();
    }

    public void RemoveItem(Item item)
    {
        item.OnRemoved(this);
        if (collectedItems.ContainsKey(item))
            collectedItems[item]--;
        else
            collectedItems.Remove(item);
        UpdateStats();
    }

    //Handles updating all the attributes this script handles.
    public void UpdateStats()
    {
        _plrController.speed = Attributes[Enums.Attributes.MovementSpeed];
    }

    IEnumerator StartTimers()
    {
        while (true)
        {
            foreach (var item in collectedItems)
            {
                if (item.Key is TimerItem timerItem)
                    timerItem.OnTimerTick();
                
            }
            
            yield return new WaitForSeconds(Attributes[Enums.Attributes.AttackCoolDown]);
        }
    }

    void DisplayDictionary()
    {
        string output = "";
        foreach (var item in collectedItems)
            output += $"{item.Key}: {item.Value}\n";
        
        print(output);
    }
}
