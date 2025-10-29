using System;
using System.Collections;
using System.Threading.Tasks;
using Enums;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicMissileItem", menuName = "Scriptable Objects/MagicMissileItem")]
public class MagicMissileItem : TimerItem
{
    //Modifies damage based on this value
    public float damageModifier;
    public GameObject magicMissilePrefab;

    public override void OnTimerTick()
    {
        CastMagicMissile();
    }

    void SpawnMagicMissile()
    {
        var missile = Instantiate(magicMissilePrefab);
        missile.transform.position = player.transform.position;
        var missileStats = missile.GetComponent<MagicMissileProjectile>();
        missileStats.shooter = player;
        try
        {
            missileStats.missileDamage = damageModifier *
                                         player.GetComponent<CharacterAttributes>().Attributes[Attributes.AttackDamage];
        }
        catch
        {
            missileStats.missileDamage = 5;
        }
    }

    public async void CastMagicMissile()
    {
        try
        {
            int numberOfMissiles = Owner.collectedItems[this];
            for (int i = 0; i < numberOfMissiles; i++)
            {
                SpawnMagicMissile();
                await Task.Delay(100);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            throw; // TODO handle exception
        }
    }
}
