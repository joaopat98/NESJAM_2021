using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : EnemyBase<Flyer>
{
    public float flightRange;
    public float waveAmplitude;
    public float wavePeriod;
    [HideInInspector] public Vector3 startPosition;

    protected override void Start()
    {
        base.Start();
        state = FlyerFlying.Create(this);
        startPosition = transform.position;
    }
}
