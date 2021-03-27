using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caterpillar : EnemyBase<Caterpillar>
{
    protected override void Start()
    {
        base.Start();
        state = CaterpillarIdle.Create(this);
    }
}
