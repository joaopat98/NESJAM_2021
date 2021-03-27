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
        player.animations.Buster.OnStateExit.AddListener(() => player.movement.Release(this));
    }

    void Update()
    {
        //On Key up Fire
        if (FireEnded)
        {
            player.animations.ShootBuster = true;
            if (player.controller.isGrounded)
                player.movement.Lock(this);
        }
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
            chargeShot.StartTimer();
        }
        if (previousFire && !currentFire)
        {
            FireEnded = true;
            chargeShot.EndTimer(bulletSpawn);
        }
        previousFire = currentFire;
    }
}
