using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet: Bullet
{
    protected override void Hit(GameObject target)
    {
        PlayerEntity player = PlayerEntity.instance;
        player.health.Hit(1);
    }
}