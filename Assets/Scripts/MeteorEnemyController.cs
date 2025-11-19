using System;
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
        print("Meteor Attack!");
    }
}
