using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicMissileItem", menuName = "Scriptable Objects/MagicMissileItem")]
public class MagicMissileItem : TimerItem
{
    //Modifies damage based on this value
    public float damageModifier;
    public GameObject magicMissilePrefab;

    public override void OnTimerTick()
    {
        var missile = Instantiate(magicMissilePrefab);
        missile.transform.position = player.transform.position;
        var missileStats = missile.GetComponent<MagicMissileProjectile>();
        missileStats.shooter = player;
        missileStats.missileDamage = damageModifier * player.GetComponent<CharacterAttributes>().Attributes[Attributes.AttackDamage];
    }
}
