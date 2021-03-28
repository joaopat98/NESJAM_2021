using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedEnemy : EnemyBase<FixedEnemy>
{
    [SerializeField] int Damage;
    protected override void Start()
    {
        base.Start();
        state = FixedEnemyIdle.Create(this);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerEntity.instance.health.Hit(Damage);
        }
    }


}
