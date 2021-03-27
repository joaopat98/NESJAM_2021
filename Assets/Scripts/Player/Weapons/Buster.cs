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

    private ChargeShot chargeShot;
    [SerializeField] private Transform bulletSpawn;

    void Awake()
    {
        chargeShot = GetComponent<ChargeShot>();
    }

    public override void Init(PlayerEntity player)
    {
        base.Init(player);
        //player.animations.Buster.OnStateExit.AddListener(() => player.movement.Release(this));
    }

    void Update()
    {
        AddTimeSinceLastFire(Time.deltaTime);
        //On Key up Fire
        if (FireEnded)
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        chargeShot.EndTimer(bulletSpawn);
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
            if (GetReadyToFire())
            {
                FireStarted = true;
                player.animations.PrepareBuster = true;
                chargeShot.StartTimer();

                ResetTimeSinceLastFire();
            }
        }
        if (previousFire && !currentFire)
        {
            if (GetAbleToFire())
            {
                FireEnded = true;
                player.animations.PrepareBuster = false;
            }
        }
        previousFire = currentFire;
    }
}
