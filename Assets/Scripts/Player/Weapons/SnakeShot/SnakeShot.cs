using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeShot : Weapon
{
    bool FireStarted;
    bool FireEnded;
    bool previousFire = false;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform BulletSpawn;
    [SerializeField] LayerMask GroundMask;

    void Awake()
    {
    }

    void Update()
    {
        //On Key up Fire
        if (FireEnded)
        {
            FireBullet();
        }
    }

    protected override void FireBullet()
    {
        base.FireBullet();
        Vector2 rayOrigin = new Vector2(transform.position.x, BulletSpawn.position.y);
        float rayDistance = Mathf.Abs(BulletSpawn.position.x - transform.position.x);
        RaycastHit2D wallHit = Physics2D.Raycast(rayOrigin, transform.right, rayDistance, GroundMask);
        Vector2 spawnPos = BulletSpawn.position;
        if (wallHit.collider != null)
        {
            spawnPos.x = transform.position.x;
        }
        var bullet = Instantiate(BulletPrefab, spawnPos, Quaternion.identity).GetComponent<Bullet>();
        bullet.Init(transform.right);
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
        }
        if (previousFire && !currentFire)
        {
            FireEnded = true;
        }
        previousFire = currentFire;
    }
}
