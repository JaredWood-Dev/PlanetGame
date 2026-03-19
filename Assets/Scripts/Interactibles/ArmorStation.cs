using UnityEngine;

public class ArmorStation : Interactable
{
    /*
     * The armor station allows the player to exhange Starbits to repair their armor.
     */

    public int amount;
    private ParticleSystemController _system;

    void Start()
    {
        _system = GetComponent<ParticleSystemController>();
        DefaultSprite = highlight;
    }
    public override void Interact()
    {
        if (player)
        {
            player.GetComponent<Armor>().RestoreArmor(amount);
            _system.StartSystem();
        }
    }
}
