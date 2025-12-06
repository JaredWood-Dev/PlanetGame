using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Health : MonoBehaviour
{
    /*
     * GameObjects with this script can be damaged, healed and possibly, killed.
     */

    [Header("Health and Regeneration")]
    public float maxHealth;
    public float currentHealth;
    public float regenRate;
    
    [Header("Defenses")]
    public float defense;
    [Range(-1, 1)]
    public float knockbackResistance;
    public List<Enums.DamageType> resistances;
    public List<Enums.DamageType> vulnerabilities;
    public List<Enums.DamageType> immunities;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Material _defaultMaterial;
    public Material spriteFlash;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        _sr = GetComponent<SpriteRenderer>();
        _defaultMaterial = _sr.material;
    }
    
    public void ChangeHealth(float amount, GameObject source = null)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth <= 0)
            KillCreature(source);
    }

    public void Damage(float amount, DamageType damageType, Vector2 knockback, GameObject source = null)
    {
        
        float damageAmount = Mathf.Max(amount - defense, 0);

        foreach (DamageType damage in resistances)
            if (damageType == damage)
                damageAmount /= 2;
        foreach (DamageType damage in vulnerabilities)
            if (damageType == damage)
                damageAmount *= 2;
        foreach (DamageType damage in immunities)
            if (damageType == damage)
                damageAmount = 0;

        float knockBackMultiplier = 1 - knockbackResistance;
        _rb.AddForce(knockback * (knockBackMultiplier * _rb.mass), ForceMode2D.Impulse);
        
        ChangeHealth(-damageAmount, source);
        
        if (gameObject.CompareTag("Player"))
            EventManager.PlayerDamaged(source, gameObject, damageAmount);
        else
            EventManager.EnemyDamaged(source, gameObject, damageAmount);
        
        _sr.material = spriteFlash;
        Invoke("ResetMaterial", 0.1f);
    }

    public void Heal(float amount)
    {
        
        ChangeHealth(amount);
        
        if (gameObject.CompareTag("Player"))
            EventManager.PlayerHealed(gameObject, amount);
    }

    void KillCreature(GameObject source = null)
    {
        //play death animation
        
        if (gameObject.CompareTag("Player"))
            EventManager.PlayerDeath();
        else
            EventManager.EnemyDeath(source, gameObject);
        Destroy(gameObject);
    }
    
    
    void OnEnable()
    {
        if (gameObject.CompareTag("Player"))
            EventManager.UpdateStats += UpdateStats;
    }

    void OnDisable()
    {
        if (gameObject.CompareTag("Player"))
            EventManager.UpdateStats -= UpdateStats;
    }

    public void UpdateStats(Attributes stat, float value)
    {
        switch (stat)
        {
            case Attributes.MaxHealth: maxHealth = value;break;
            case Attributes.Defense: defense = value;break;
            case Attributes.HeathRegen: regenRate = value;break;
        }
    }

    void ResetMaterial()
    {
        _sr.material = _defaultMaterial;
    }
}
