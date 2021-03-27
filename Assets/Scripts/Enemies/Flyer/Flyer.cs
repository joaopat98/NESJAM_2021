using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : EnemyBase<Flyer>
{
    protected override void Start()
    {
        base.Start();
        state = FlyerFlying.Create(this);
    }
}
