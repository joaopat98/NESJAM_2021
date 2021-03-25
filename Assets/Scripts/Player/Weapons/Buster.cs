using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Buster : Weapon
{
    bool FireStarted;
    bool FireEnded;
    bool previousFire = false;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform BulletSpawn;

    private ChargeShot chargeShot;

    void Awake()
    {
        chargeShot = GetComponent<ChargeShot>();
    }

    public override void Init(PlayerEntity player)
    {
        base.Init(player);
        player.animations.Buster.OnStateExit.AddListener(() => player.movement.Release(this));
    }

    void Update()
    {
        if (previousFire) { chargeShot.IncreaseTimer(); }
        //On Key up Fire
        if (FireEnded)
        {
            FireBullet();
            player.animations.ShootBuster = true;
            if (player.controller.isGrounded)
                player.movement.Lock(this);
        }
    }

    private void FireBullet()
    {
        var bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity).GetComponent<BusterBullet>();
        bullet.Init(transform.right);
        if (chargeShot.GetFullChargedShot()) { bullet.FullCharge(); }
        else if (chargeShot.GetMidChargedShot()) { bullet.MidCharge(); }
    }

    void LateUpdate()
    {
        FireStarted = false;
        FireEnded = false;
    }

    protected override void OnFire(InputValue value)
    {
        bool currentFire = value.Get<float>() == 1;
        if (!previousFire && currentFire)
        {
            FireStarted = true;
            chargeShot.ResetTimer();
        }
        if (previousFire && !currentFire)
        {
            FireEnded = true;
        }
        previousFire = currentFire;
    }
}
