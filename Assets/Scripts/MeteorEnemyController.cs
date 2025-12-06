using System;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteorEnemyController : EnemyController
{
    public override void EnemyIdle()
    {
        _aiPath.target = transform.position;
    }

    public override void EnemyChase()
    {
        _aiPath.target = player.position;
    }

    public override void EnemyAttack()
    {
        if (player)
            player.gameObject.GetComponent<Health>().Damage(damage, DamageType.Bludgeoning, player.up * 10, gameObject);
        print("Meteor Attack!");
    }
}
