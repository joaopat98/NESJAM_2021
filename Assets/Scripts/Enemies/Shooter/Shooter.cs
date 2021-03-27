using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyBase<Shooter>
{
    protected override void Start()
    {
        base.Start();
        state = ShooterIdle.Create(this);
    }
}
