using UnityEngine;

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

    void Start()
    {
        currentArmorSlots = armorSlots;
    }

    void Hit(int damage, Vector2 knockback)
    {
        _rb.AddForce(knockback *  (1 - (knockbackResistance * 100)), ForceMode2D.Impulse);
        if (armorSlots <= 0)
        {
            //Creature dies    
            Destroy(gameObject);
            return;
        }
        
        if (currentArmorSlots - damage <= 0)
        {
            currentArmorSlots = 0;
        }
    }

    void RestoreArmor(int amount)
    {
        if (currentArmorSlots + amount >= armorSlots)
        {
            currentArmorSlots = armorSlots;
        }
        currentArmorSlots += amount;
    }
}
