using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyBase<Shooter>
{
    public float shootingCooldown = 3;
    [HideInInspector] public float cooldownLeft;
    protected override void Start()
    {
        base.Start();
        state = ShooterIdle.Create(this);
    }
}
