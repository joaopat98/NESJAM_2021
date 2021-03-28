using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DownGun : Weapon
{
    bool FireStarted;
    bool FireEnded;
    bool ShotAvailable = true;
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
        if (FireEnded && ShotAvailable)
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        var bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity).GetComponent<DownGunBullet>();
        bullet.Init(Vector3.down, this);
        player.movement.Jump(JumpBoost);
        ShotAvailable = false;
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

    public void OnShotDestroyed()
    {
        ShotAvailable = true;
    }
}
