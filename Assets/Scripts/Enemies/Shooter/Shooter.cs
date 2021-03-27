using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyBase<Shooter>
{
    public float shotNumber = 3;
    public float fireRate = 1;
    public float shootingCooldown = 3;
    public float TimeToPrepare = 0.5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public bool facingRight = false;
    public bool shield = false;
    protected override void Start()
    {
        base.Start();
        state = ShooterIdle.Create(this);
    }
}
