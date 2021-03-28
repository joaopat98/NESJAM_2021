using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DownGun : Weapon
{
    bool FireStarted;
    bool FireEnded;
    bool previousFire = false;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform BulletSpawn;
    [SerializeField] float JumpBoost = 3;

    void Awake()
    {
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

    protected override void FireBullet()
    {
        base.FireBullet();
        var bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Init(Vector3.down);
        player.movement.Jump(JumpBoost);
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
                ResetTimeSinceLastFire();
            }
        }
        if (previousFire && !currentFire)
        {
            if (GetAbleToFire())
            {
                FireEnded = true;
            }
        }
        previousFire = currentFire;
    }
}
