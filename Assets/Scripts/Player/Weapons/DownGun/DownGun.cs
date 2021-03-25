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

    PlayerMovement playerMovement;

    Transform playerTransform;

    void Awake(){
        playerTransform = transform.parent.parent;
        playerMovement = playerTransform.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //On Key up Fire
        if (FireEnded)
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        var bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity).GetComponent<DownGunBullet>();
        playerMovement.AddVerticalVel(bullet.GetBoostHeight());
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
