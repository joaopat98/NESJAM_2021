using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DualShot : Weapon
{
    bool FireStarted;
    bool FireEnded;
    bool previousFire = false;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform BulletSpawn;

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

    private void FireBullet()
    {
        var bullet1 = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity).GetComponent<Bullet>();
        var bullet2 = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity).GetComponent<Bullet>();
        bullet1.Init(Vector3.up);
        bullet2.Init(Vector3.down);
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
