using UnityEngine;
using UnityEngine.SceneManagement;

public class Armor : MonoBehaviour
{
    /*
     * This script allows the given creature to take damage.
     * If a creature takes a hit, it knocks out an armor slot.
     * The creature dies if it takes a hit while having no armor slots.
     * Notably, if a creature takes more damage than remaining armor slots - it is fine.
     */

    public int armorSlots;
    public int currentArmorSlots;
    
    //This number represents how well a creature can resist knockback
    [Range(-1, 1)]
    public float knockbackResistance;
    
    //This is the amount of time it takes to automatically regenerate an armor slot
    public int regenerationRate;

    private Rigidbody2D _rb;
    
    //Materials to give feedback when hit
    [Header("Flash Effect")]
    private Material _defaultMaterial;
    public Material flashMaterial;

    [Header("Sound Effects")]
    public AudioClip hitSound;
    private AudioSource _hitSource;

    void Start()
    {
        currentArmorSlots = armorSlots;
        _rb = GetComponent<Rigidbody2D>();
        
        _defaultMaterial = GetComponent<Renderer>().material;
        
        _hitSource = gameObject.GetComponents<AudioSource>()[2];
    }

    public void Hit(int damage, GameObject attacker = null, Vector2 knockback = new Vector2())
    {
        GetComponent<Renderer>().material = flashMaterial;

        if (_hitSource)
        {
            float pitchVariance = Random.Range(0.8f, 1.2f);
            _hitSource.pitch = pitchVariance;
            _hitSource.PlayOneShot(hitSound);
        }
        
        _rb.AddForce(knockback, ForceMode2D.Impulse);
        if (currentArmorSlots <= 0)
        {
            //Creature dies    
            DestroyCreature();
            return;
        }
        
        if (currentArmorSlots - damage <= 0)
        {
            currentArmorSlots = 0;
        }
        else
        {
            currentArmorSlots -= damage;
        }
        EventManager.ArmorHit(gameObject, attacker, currentArmorSlots);
        
        Invoke(nameof(ResetMaterial), 0.1f);
    }

    public void RestoreArmor(int amount)
    {
        if (currentArmorSlots + amount >= armorSlots)
        {
            currentArmorSlots = armorSlots;
        }
        else
        {
            currentArmorSlots += amount;
        }
        EventManager.ArmorHit(gameObject, gameObject, currentArmorSlots);
    }

    void DestroyCreature()
    {
        if (gameObject.CompareTag("Player"))
        {
            //Game Over!
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetMaterial()
    {
        if (gameObject && GetComponent<Renderer>())
            GetComponent<Renderer>().material = _defaultMaterial;
    }
}
