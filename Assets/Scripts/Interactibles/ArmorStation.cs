using UnityEngine;

public class ArmorStation : Interactable
{
    /*
     * The armor station allows the player to exhange Starbits to repair their armor.
     */

    public int amount;
    public override void Interact()
    {
        if (player)
            player.GetComponent<Armor>().RestoreArmor(amount);
    }
}
