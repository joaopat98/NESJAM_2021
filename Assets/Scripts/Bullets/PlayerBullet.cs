using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField] int Damage = 5;
    protected override void Hit(GameObject target)
    {
        EnemyBase enemy = target.GetComponent<EnemyBase>();
        enemy.Hit(Damage);
    }
}
